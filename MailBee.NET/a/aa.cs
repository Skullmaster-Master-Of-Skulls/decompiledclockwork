using System;
using System.Security.Cryptography;
using System.Text;
using MailBee;

namespace a
{
	// Token: 0x02000480 RID: 1152
	internal class aa : SaslMethod
	{
		// Token: 0x060027C4 RID: 10180 RVA: 0x000B8421 File Offset: 0x000B7421
		public override string GetSaslID()
		{
			return "CRAM-MD5";
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x000B8428 File Offset: 0x000B7428
		internal override AuthenticationMethods GetMethodEnumMember()
		{
			return AuthenticationMethods.SaslCramMD5;
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x000B842C File Offset: 0x000B742C
		public override void CreateNextClientAnswer()
		{
			if (base.Stage == 0)
			{
				base.ClientAnswer = base.ClientAnswerEncoding.GetBytes(this.b());
				int stage = base.Stage;
				base.Stage = stage + 1;
			}
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x000B846A File Offset: 0x000B746A
		public override bool IsSecure()
		{
			return true;
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x000B846D File Offset: 0x000B746D
		public override bool IsFipsCompliant()
		{
			return false;
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x000B8470 File Offset: 0x000B7470
		private new string b()
		{
			byte[] array = this.a();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].ToString("x");
				stringBuilder.Append((text.Length > 1) ? text : ("0" + text));
			}
			return string.Format("{0} {1}", base.AccountName, stringBuilder.ToString());
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x000B84E0 File Offset: 0x000B74E0
		private new byte[] a()
		{
			byte[] array = new byte[0];
			int num = 64;
			byte[] array2 = new byte[num];
			byte[] bytes = base.ClientAnswerEncoding.GetBytes(base.Password);
			if (bytes.Length < num)
			{
				Array.Copy(bytes, 0, array2, 0, bytes.Length);
			}
			byte[] array3 = new byte[num];
			byte[] array4 = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array3[i] = Convert.ToByte((int)(array2[i] ^ Convert.ToByte(54)));
				array4[i] = Convert.ToByte((int)(array2[i] ^ Convert.ToByte(92)));
			}
			byte[] array5 = new byte[array3.Length + base.ServerChallenge.Length];
			Array.Copy(array3, 0, array5, 0, array3.Length);
			Array.Copy(base.ServerChallenge, 0, array5, array3.Length, base.ServerChallenge.Length);
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			array5 = md5CryptoServiceProvider.ComputeHash(array5);
			array = new byte[array4.Length + array5.Length];
			Array.Copy(array4, 0, array, 0, array4.Length);
			Array.Copy(array5, 0, array, array4.Length, array5.Length);
			return md5CryptoServiceProvider.ComputeHash(array);
		}
	}
}
