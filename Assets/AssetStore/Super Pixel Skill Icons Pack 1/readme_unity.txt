//////////
// INFO //
//////////

Thank you for your purchase and download!
Support me on Patreon and get tons of awesome pixel art: http://patreon.com/untiedgames

MORE LINKS:
Browse my other assets: untiedgames.itch.io
More of my Unity-ready assets: assetstore.unity.com/publishers/18465
Watch me make pixel art, games, and more: youtube.com/c/unTiedGamesTV
Follow on Mastodon: mastodon.gamedev.place/@untiedgames
Follow on Facebook: facebook.com/untiedgames
Visit my blog: untiedgames.com
Newsletter signup: http://untiedgames.com/signup

/////////////////////
// VERSION HISTORY //
/////////////////////

Version 1.0
	- Initial release. Woohoo!

////////////////////////////////
// HOW TO USE THIS ASSET PACK //
////////////////////////////////

Hello! Thank you for downloading the Super Pixel Skill Icons Pack 1 asset pack. Here are a few pointers to help you navigate and understand this asset.

- In the PNG folder, you will find the PNG images for all of the assets in the pack.

- The Prefabs folder contains prefabs for all animated assets, ready to go!

- The Animations folder contains all animations used in each prefab.

- The autocast, cooldown, and highlight animations are designed to be overlaid on the icons.

- The cooldown animation is provided at full opacity, so you can customize how transparent/opaque it is in your game.

- To display the correct frame of the cooldown animation:
	1. Assume the ability has two variables: cooldown_current and cooldown_max, where:
		* cooldown_max represents the maximum cooldown value. (e.g. if your ability goes on cooldown for 10 seconds, then cooldown_max = 10)
		* cooldown_current represents a timer that goes down linearly with time. (e.g. when you use the ability, cooldown_current gets set to cooldown_max and then decreases at a rate of 1 per second in your game loop)
	2. The cooldown animation is 100 frames, to make it easy to convert percentage to frame. We can get a percentage with this formula:
		* percent = (1.0f - cooldown_current / cooldown_max)
		  (The percentage is inverted, because the animation starts at full cooldown)
	3. With the percentage, multiply by the length of the animation (100 frames).
		* frame = round(percent * 100.0f)
	4. Display the frame of the animation indicated by the calculation- All done!

	Example:
		Ability has cooldown of 10 seconds, and 3 seconds have elapsed since it was used.
		cooldown_max = 10.0
		cooldown_current = 7.0
		percent = (1.0f - cooldown_current / cooldown_max) // value: 0.3
		frame = round(percent * 100.0f) // value: 30
		// Display frame 30

- Recommended animation FPS: 30 (34 ms/frame)

Any questions?
Email me at contact@untiedgames.com and I'll try to answer as best I can!

-Will