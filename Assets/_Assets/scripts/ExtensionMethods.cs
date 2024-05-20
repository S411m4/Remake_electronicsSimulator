using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods 
{
    public static int CharToInt(this char value)
    {
        
        switch (value)
        {
            case '0':
                return 0;
            case '1':
                return 1;
            case '2':
                return 2;
            case '3':
                return 3;
            case '4':
                return 4;
            case '5':
                return 5;
            case '6':
                return 6;
            case '7':
                return 7;
            case '8':
                return 8;
            case '9':
                return 9;
        }

        return 0;
    }

    
    public static float Map( this float value, float from1, float from2, float to1, float to2)
    {
        return ( value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static double ConvertToUnit(this float value, char unitConversion)
    {
        switch(unitConversion)
        {
            case 'm': //milli (10^-3)
                return value * (1e-3);


            case 'u': //micro (10^-6)
                return value * (1e-6);


            case 'n': //nano (10^-9)
                return value * (1e-9);


            case 'K': //kilo (10^3)
                return value * (1e+3);


            case 'M': //mega (10^6)
                return value * (1e+6);


            case 'G': //Giga (10^9)
                return value * (1e+9);
 

            default:
                return value;


        }
    }
}
