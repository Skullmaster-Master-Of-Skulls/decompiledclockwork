using System;
using System.Security.Cryptography;

namespace System.IdentityModel
{
	// Token: 0x02000069 RID: 105
	internal sealed class Psha1DerivedKeyGenerator
	{
		// Token: 0x06000339 RID: 825 RVA: 0x0000C790 File Offset: 0x0000A990
		public Psha1DerivedKeyGenerator(byte[] key)
		{
			if (key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
			}
			this.key = key;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		public byte[] GenerateDerivedKey(byte[] label, byte[] nonce, int derivedKeySize, int position)
		{
			if (label == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("label");
			}
			if (nonce == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("nonce");
			}
			Psha1DerivedKeyGenerator.ManagedPsha1 managedPsha = new Psha1DerivedKeyGenerator.ManagedPsha1(this.key, label, nonce);
			return managedPsha.GetDerivedKey(derivedKeySize, position);
		}

		// Token: 0x0400035D RID: 861
		private byte[] key;

		// Token: 0x02000237 RID: 567
		private sealed class ManagedPsha1
		{
			// Token: 0x06001204 RID: 4612 RVA: 0x0004ED48 File Offset: 0x0004CF48
			public ManagedPsha1(byte[] secret, byte[] label, byte[] seed)
			{
				this.secret = secret;
				checked
				{
					this.seed = DiagnosticUtility.Utility.AllocateByteArray(label.Length + seed.Length);
					label.CopyTo(this.seed, 0);
					seed.CopyTo(this.seed, label.Length);
					this.aValue = this.seed;
					this.chunk = new byte[0];
					this.index = 0;
					this.position = 0;
					this.hmac = CryptoHelper.NewHmacSha1KeyedHashAlgorithm(secret);
					this.buffer = DiagnosticUtility.Utility.AllocateByteArray(this.hmac.HashSize / 8 + this.seed.Length);
				}
			}

			// Token: 0x06001205 RID: 4613 RVA: 0x0004EDF0 File Offset: 0x0004CFF0
			public byte[] GetDerivedKey(int derivedKeySize, int position)
			{
				if (derivedKeySize < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("derivedKeySize", SR.GetString("ValueMustBeNonNegative")));
				}
				if (this.position > position)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("position", SR.GetString("ValueMustBeInRange", new object[]
					{
						0,
						this.position
					})));
				}
				while (this.position < position)
				{
					this.GetByte();
				}
				int num = derivedKeySize / 8;
				byte[] array = new byte[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.GetByte();
				}
				return array;
			}

			// Token: 0x06001206 RID: 4614 RVA: 0x0004EE98 File Offset: 0x0004D098
			private byte GetByte()
			{
				if (this.index >= this.chunk.Length)
				{
					this.hmac.Initialize();
					this.aValue = this.hmac.ComputeHash(this.aValue);
					this.aValue.CopyTo(this.buffer, 0);
					this.seed.CopyTo(this.buffer, this.aValue.Length);
					this.hmac.Initialize();
					this.chunk = this.hmac.ComputeHash(this.buffer);
					this.index = 0;
				}
				this.position++;
				byte[] array = this.chunk;
				int num = this.index;
				this.index = num + 1;
				return array[num];
			}

			// Token: 0x04000F4A RID: 3914
			private byte[] aValue;

			// Token: 0x04000F4B RID: 3915
			private byte[] buffer;

			// Token: 0x04000F4C RID: 3916
			private byte[] chunk;

			// Token: 0x04000F4D RID: 3917
			private KeyedHashAlgorithm hmac;

			// Token: 0x04000F4E RID: 3918
			private int index;

			// Token: 0x04000F4F RID: 3919
			private int position;

			// Token: 0x04000F50 RID: 3920
			private byte[] secret;

			// Token: 0x04000F51 RID: 3921
			private byte[] seed;
		}
	}
}
