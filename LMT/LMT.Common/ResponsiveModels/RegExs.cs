using System;
using System.Collections.Generic;
using System.Text;

namespace LMT.Common.ResponsiveModels
{
	public class RegExs
	{
		public static string GenericName = @"^[a-zA-Z0-9\s-.]+$";
		public static string IFSCode = @"[A-Z|a-z]{4}[0][\d]{6}$";
		public static string Username = @"^[A-Za-z0-9]+(?:[_.][A-Za-z0-9]+)*$";
		public static string PANCard = @"^[\w]{3}(p|P|c|C|h|H|f|F|a|A|t|T|b|B|l|L|j|J|g|G)[\w][\d]{4}[\w]$";
		public static string PasswordPolicy = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,255}$";
		public static string Name = @"^[a-zA-Z0-9 ]+(([',. -][a-zA-Z0-9 ])?[a-zA-Z0-9 ]*)*$";
		public static string Aadhar = @"^\d{4}\s\d{4}\s\d{4}$";
		public static string Mobile = @"^(\+91[\-\s]?)?[0]?(91)?[789]\d{9}$";
		public static string PINCode = @"^[1-9][0-9]{6}$";
		public static string Decimal = @"^(\d*\.)?\d+$";
		public static string Address = @"^[a-zA-Z0-9 \n\r @.,#\/\&+-]+$";
	}
}
