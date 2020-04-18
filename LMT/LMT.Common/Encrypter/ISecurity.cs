using System;
using System.Collections.Generic;
using System.Text;

namespace LMT.Common.Encrypter
{
	public interface ISecurity
	{
		string Encrypt(string key, string data);
		string Decrypt(string key, string data);
		string EncryptionKey();

	}
}
