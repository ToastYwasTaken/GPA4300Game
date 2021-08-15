using System;
using System.Globalization;
using UnityEngine;


namespace CustomExtensions
{

    /******************************************************************************
    * Project: GPA4300Game
    * File: Item.cs
    * Version: 1.0
    * Autor: René Kraus (RK); Franz Mörike (FM); Jan Pagel (JP)
    * 
    * 
    * These coded instructions, statements, and computer programs contain
    * proprietary information of the author and are protected by Federal
    * copyright law. They may not be disclosed to third parties or copied
    * or duplicated in any form, in whole or in part, without the prior
    * written consent of the author.
    * 
    * ChangeLog
    * ----------------------------
    *  11.08.2021  RK  erstellt
    *  
    *****************************************************************************/
    public static class StringExtensions
    {
        /// <summary>
        /// Convert a vector 3 string "(#.#, #.#, #.#)" to a Vector3 value 
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Vector3</returns>
        public static Vector3 ToVector3(this string str)
        {
            /// erwartetes Format: (35.0, 21.0, 432.0) von Vector3.ToString()

            // String Argument prüfen
            if (!str.StartsWith("(") && !str.EndsWith(")") && !str.Contains(","))
                throw new ArgumentException("Format invalid!");

            // Klammern entfernen
            string strVector = str.Remove(str.Length - 1, 1).Remove(0, 1);

            // String in die einzelnen Vektorwerte aufteilen
            string[] strArray = strVector.Split(',');

            // Float Array für die einzelnen Vektorwerte
            float[] vectorValues = new float[strArray.Length];

            // string-Werte in float-Werte Parsen
            for (int i = 0; i < vectorValues.Length; i++)
            {
                vectorValues[i] = float.Parse(strArray[i], CultureInfo.InvariantCulture);
            }

            // neuen Vektor 3 zurückgeben
            return new Vector3(vectorValues[0], vectorValues[1], vectorValues[2]);
        }
    }
}