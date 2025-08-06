using UnityEngine;


namespace GameSystem.DamageFormat
{
    public static class UnitFormatter
    {
        private static readonly string[] Units = { "", "A", "B", "C", "D", "E", "F", "G" };

        public static string FormatWithUnit(double value)
        {
            int unitIndex = 0;

            // 10 단위로 단위 승급
            while (value >= 10 && unitIndex < Units.Length - 1)
            {
                value /= 10;
                unitIndex++;
            }

            return $"{value:0.##}{Units[unitIndex]}";
        }
    }
}