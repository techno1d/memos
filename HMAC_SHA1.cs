public static string Encode(string input, string key)
{			
	HMACSHA1 myhmacsha1 = new HMACSHA1(Encoding.ASCII.GetBytes(key));
	byte[] byteArray = Encoding.ASCII.GetBytes(input);
	
	MemoryStream stream = new MemoryStream(byteArray);
	return Convert.ToBase64String( myhmacsha1.ComputeHash(stream));
}		
