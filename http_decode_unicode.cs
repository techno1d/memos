using System.Globalization;
using System.Text.RegularExpressions;

namespace Memos
{
	public static partial class Helper
	{
		public static string DecodeUnicodeString(this string responseString)
		{
			string result = Regex.Replace(
			  responseString,
			  @"\\[Uu]([0-9A-Fa-f]{4})",
			  m => char.ToString((char)ushort.Parse(m.Groups[1].Value, NumberStyles.AllowHexSpecifier)));

			return result;
		}
	}
}
