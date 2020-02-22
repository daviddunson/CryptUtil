// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ByteArray.cs" company="GSD Logic">
//   Copyright © 2020 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CryptUtil
{
    public static class ByteArray
    {
        public static int IndexOf(this byte[] bytes, byte[] value)
        {
            if ((bytes != null) && (value != null) && (bytes.Length >= value.Length))
            {
                for (int i = 0, j = 0; i < bytes.Length; i++)
                {
                    if (bytes[i] == value[j])
                    {
                        if (++j >= value.Length)
                        {
                            return i - j + 1;
                        }
                    }
                    else
                    {
                        j = 0;
                    }
                }
            }

            return -1;
        }
    }
}