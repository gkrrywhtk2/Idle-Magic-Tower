using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CollectionScripts;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace Assets.PixelFantasy.PixelHeroes.Common.Scripts.EditorScripts
{
    public class CharacterEditor : MonoBehaviour
    {
        public SpriteCollection SpriteCollection;
        public List<LayerEditor> Layers;
        public CharacterBuilder CharacterBuilder;
        public Sprite EmptyIcon;

        public static event Action<string> SliceTextureRequest = _ => {};
        public static event Action<string> CreateSpriteLibraryRequest = _ => { };

        public void Start()
        {
            foreach (var layer in Layers)
            {
                if (layer.Controls)
                {
                    if (layer.Default != "")
                    {
                        layer.Index = SpriteCollection.Layers.Single(i => i.Name == layer.Name).Textures.FindIndex(i => i.name == layer.Default);
                    }
                    else
                    {
                        layer.Index = -1;
                    }

                    layer.Content = SpriteCollection.Layers.Single(i => i.Name == layer.Name);
                    layer.Controls.Dropdown.options = new List<Dropdown.OptionData>();

                    if (layer.CanBeEmpty) layer.Controls.Dropdown.options.Add(new Dropdown.OptionData("Empty", EmptyIcon));

                    layer.Controls.Dropdown.options.AddRange(layer.Content.Textures.Select(i => new Dropdown.OptionData(GetDisplayName(i.name), Sprite.Create(layer.Content.GetIcon(i), new Rect(0, 0, 32, 32), Vector2.one / 2, 100))));
                    layer.Controls.Dropdown.value = -1;
                    layer.Controls.Dropdown.value = layer.Index + (layer.CanBeEmpty ? 1 : 0);
                    layer.Controls.Dropdown.onValueChanged.AddListener(value => SetIndex(layer, value));
                    layer.Controls.Prev.onClick.AddListener(() => Switch(layer, -1));
                    layer.Controls.Next.onClick.AddListener(() => Switch(layer, +1));
                    layer.Controls.Hide.onClick.AddListener(() => Hide(layer));
                    layer.Controls.Paint.onClick.AddListener(() => Paint(layer));
                    layer.Controls.Hue.onValueChanged.AddListener(value => Rebuild());
                    layer.Controls.Saturation.onValueChanged.AddListener(value => Rebuild());
                    layer.Controls.Brightness.onValueChanged.AddListener(value => Rebuild());
                    layer.Controls.OnSelectFixedColor = color => Repaint(layer, color);
                }
            }

            Rebuild();
        }

        private void Rebuild()
        {
            var layers = Layers.ToDictionary(i => i.Name, i => i.SpriteData);

            CharacterBuilder.Head = layers["Head"];
            CharacterBuilder.Ears = layers["Ears"];
            CharacterBuilder.Eyes = layers["Eyes"];
            CharacterBuilder.Body = layers["Body"];
            CharacterBuilder.Hair = layers["Hair"];
            CharacterBuilder.Armor = layers["Armor"];
            CharacterBuilder.Helmet = layers["Helmet"];
            CharacterBuilder.Weapon = layers["Weapon"];
            CharacterBuilder.Firearm = layers["Firearm"];
            CharacterBuilder.Shield = layers["Shield"];
            CharacterBuilder.Cape = layers["Cape"];
            CharacterBuilder.Back = layers["Back"];
            CharacterBuilder.Mask = layers["Mask"];
            CharacterBuilder.Horns = layers["Horns"];
            CharacterBuilder.Rebuild();
        }

        public void Hide(LayerEditor layer)
        {
            layer.Hidden = !layer.Hidden;
            Rebuild();
        }

        public void Paint(LayerEditor layer)
        {
            #if UNITY_EDITOR

            ColorPicker.Open(layer.Color);
            ColorPicker.OnColorPicked = color => { layer.Color = color; Rebuild(); };

            #endif
        }

        public void Repaint(LayerEditor layer, Color color)
        {
            layer.Color = color;

            if (layer.Name == "Body")
            {
                Layers.Single(i => i.Name == "Head").Color = color;
                Layers.Single(i => i.Name == "Ears").Color = color;
            }

            if (layer.Name == "Head")
            {
                Layers.Single(i => i.Name == "Ears").Color = color;
            }

            Rebuild();
        }

        public void Preset(string preset)
        {
            var body = Layers.Single(i => i.Name == "Body");
            var dropdown = body.Controls.Dropdown;
            var index = dropdown.options.FindIndex(i => i.text == GetDisplayName(preset));

            body.Color = Color.white;

            if (index == -1)
            {
                dropdown.value = 0;
            }
            else if (index == dropdown.value)
            {
                dropdown.onValueChanged.Invoke(index);
            }
            else
            {
                dropdown.value = index;
            }
            
            Layers.Single(i => i.Name == "Body").Color = Color.white;
            Layers.Single(i => i.Name == "Head").Color = Color.white;
            Layers.Single(i => i.Name == "Ears").Color = Color.white;
            Layers.Single(i => i.Name == "Eyes").Color = Color.white;
            Layers.Single(i => i.Name == "Horns").Color = Color.white;
        }

        private void Switch(LayerEditor layer, int direction)
        {
            layer.Switch(direction);
            Rebuild();
        }

        private void SetIndex(LayerEditor layer, int index)
        {
            if (layer.CanBeEmpty) index--;

            layer.SetIndex(index);

            var caption = layer.Controls.Dropdown.options[layer.Controls.Dropdown.value].text;

            if (layer.Name == "Body")
            {
                var head = Layers.Single(i => i.Name == "Head");
                var ears = Layers.Single(i => i.Name == "Ears");
                var eyes = Layers.Single(i => i.Name == "Eyes");
                var hair = Layers.Single(i => i.Name == "Hair");
                var horns = Layers.Single(i => i.Name == "Horns");

                var headIndex = head.Controls.Dropdown.options.FindIndex(i => i.text == caption);
                var earsIndex = ears.Controls.Dropdown.options.FindIndex(i => i.text == caption);
                var eyesIndex = eyes.Controls.Dropdown.options.FindIndex(i => i.text == caption);
                var hornsIndex = horns.Controls.Dropdown.options.FindIndex(i => i.text == caption);

                if (headIndex == -1) headIndex = 0;
                if (earsIndex == -1) earsIndex = 0;
                if (eyesIndex == -1) eyesIndex = 0;
                if (hornsIndex == -1) hornsIndex = 0;

                head.Controls.Dropdown.value = headIndex;
                ears.Controls.Dropdown.value = earsIndex;
                eyes.Controls.Dropdown.value = eyesIndex;
                horns.Controls.Dropdown.value = hornsIndex;

                head.Color = layer.Color;
                ears.Color = layer.Color;

                if (caption != "Human")
                {
                    hair.Controls.Dropdown.value = 0;
                }
            }
            else if (layer.Name == "Weapon" && index != -1)
            {
                Layers.Single(i => i.Name == "Firearm").Controls.Dropdown.value = -1;
            }
            else if (layer.Name == "Firearm" && index != -1)
            {
                Layers.Single(i => i.Name == "Weapon").Controls.Dropdown.value = -1;
            }

            Rebuild();
        }

        private static string GetDisplayName(string fileName)
        {
            var displayName = Regex.Replace(fileName, @"\[\w+\]", "");

            displayName = Regex.Replace(displayName, "([a-z])([A-Z])", "$1 $2");

            return displayName.Trim();
        }

        public void Save()
        {
            CharacterBuilder.Rebuild(forceMerge: true);

            var bytes = CharacterBuilder.Texture.EncodeToPNG();

            CharacterBuilder.Rebuild(forceMerge: false);
            SaveFileDialog("Save as PNG", "SpriteSheet", "Image", ".png", bytes);
        }

        private void SaveFileDialog(string title, string fileName, string fileType, string extension, byte[] bytes)
        {
            #if UNITY_EDITOR

            var path = UnityEditor.EditorUtility.SaveFilePanel(title, null, fileName + extension, extension.Replace(".", ""));

            if (path == "") return;

            File.WriteAllBytes(path, bytes);

            if (path.StartsWith(Application.dataPath))
            {
                path = "Assets" + path.Substring(Application.dataPath.Length);
                
                AssetDatabase.Refresh();

                var importer = (TextureImporter) AssetImporter.GetAtPath(path);

                importer.textureType = TextureImporterType.Sprite;
                importer.maxTextureSize = 2048;
                importer.SaveAndReimport();

                AssetDatabase.Refresh();

                SliceTextureRequest(path);

                if (EditorUtility.DisplayDialog("Success", $"Texture saved and sliced:\n{path}\n\nDo you want to create Sprite Library Asset for it?", "Yes", "No"))
                {
                    CreateSpriteLibraryRequest(path);
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Success", $"Texture saved:\n{path}\n\nTip: textures are automatically sliced when saving to Assets.", "OK");
            }

            #elif UNITY_STANDALONE

            #if FILE_BROWSER

            StartCoroutine(SimpleFileBrowserForWindows.WindowsFileBrowser.SaveFile(title, "", fileName, fileType, extension, bytes, (success, p) => { }));
            
            #else

            Debug.LogWarning("Please import this asset: http://u3d.as/2QLg");

            #endif
            
            #elif UNITY_WEBGL

            #if FILE_BROWSER

            UI.Popup.Instance.Show("This feature is unavailable in the demo version. Please purchase the full app.");
            //SimpleFileBrowserForWebGL.WebFileBrowser.Download(fileName + extension, bytes);

            #else

            Debug.LogWarning("Please import this asset: http://u3d.as/2W52");

            #endif

            #endif
        }
    }
}