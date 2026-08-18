using System;
using System.Collections.Generic;
using Renci.SshNet.Common;

namespace Renci.SshNet.Security
{
	// Token: 0x02000075 RID: 117
	public class KeyHostAlgorithm : HostAlgorithm
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00014E48 File Offset: 0x00013048
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x00014E50 File Offset: 0x00013050
		public Key Key { get; private set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00014E59 File Offset: 0x00013059
		public override byte[] Data
		{
			get
			{
				return new KeyHostAlgorithm.SshKeyData(base.Name, this.Key.Public).GetBytes();
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00014E76 File Offset: 0x00013076
		public KeyHostAlgorithm(string name, Key key) : base(name)
		{
			this.Key = key;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00014E88 File Offset: 0x00013088
		public KeyHostAlgorithm(string name, Key key, byte[] data) : base(name)
		{
			this.Key = key;
			KeyHostAlgorithm.SshKeyData sshKeyData = new KeyHostAlgorithm.SshKeyData();
			sshKeyData.Load(data);
			this.Key.Public = sshKeyData.Keys;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00014EC1 File Offset: 0x000130C1
		public override byte[] Sign(byte[] data)
		{
			return new KeyHostAlgorithm.SignatureKeyData(base.Name, this.Key.Sign(data)).GetBytes();
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00014EE0 File Offset: 0x000130E0
		public override bool VerifySignature(byte[] data, byte[] signature)
		{
			KeyHostAlgorithm.SignatureKeyData signatureKeyData = new KeyHostAlgorithm.SignatureKeyData();
			signatureKeyData.Load(signature);
			return this.Key.VerifySignature(data, signatureKeyData.Signature);
		}

		// Token: 0x02000170 RID: 368
		private class SshKeyData : SshData
		{
			// Token: 0x170002EF RID: 751
			// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00028C98 File Offset: 0x00026E98
			// (set) Token: 0x06000D31 RID: 3377 RVA: 0x00028CE8 File Offset: 0x00026EE8
			public BigInteger[] Keys
			{
				get
				{
					BigInteger[] array = new BigInteger[this._keys.Count];
					for (int i = 0; i < this._keys.Count; i++)
					{
						byte[] data = this._keys[i];
						array[i] = data.ToBigInteger();
					}
					return array;
				}
				private set
				{
					this._keys = new List<byte[]>(value.Length);
					for (int i = 0; i < value.Length; i++)
					{
						BigInteger bigInteger = value[i];
						this._keys.Add(bigInteger.ToByteArray().Reverse<byte>());
					}
				}
			}

			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00028D32 File Offset: 0x00026F32
			// (set) Token: 0x06000D33 RID: 3379 RVA: 0x00028D4D File Offset: 0x00026F4D
			private string Name
			{
				get
				{
					return SshData.Utf8.GetString(this._name, 0, this._name.Length);
				}
				set
				{
					this._name = SshData.Utf8.GetBytes(value);
				}
			}

			// Token: 0x170002F1 RID: 753
			// (get) Token: 0x06000D34 RID: 3380 RVA: 0x00028D60 File Offset: 0x00026F60
			protected override int BufferCapacity
			{
				get
				{
					int num = base.BufferCapacity;
					num += 4;
					num += this._name.Length;
					foreach (byte[] array in this._keys)
					{
						num += 4;
						num += array.Length;
					}
					return num;
				}
			}

			// Token: 0x06000D35 RID: 3381 RVA: 0x00010840 File Offset: 0x0000EA40
			public SshKeyData()
			{
			}

			// Token: 0x06000D36 RID: 3382 RVA: 0x00028DC8 File Offset: 0x00026FC8
			public SshKeyData(string name, params BigInteger[] keys)
			{
				this.Name = name;
				this.Keys = keys;
			}

			// Token: 0x06000D37 RID: 3383 RVA: 0x00028DDE File Offset: 0x00026FDE
			protected override void LoadData()
			{
				this._name = base.ReadBinary();
				this._keys = new List<byte[]>();
				while (!base.IsEndOfData)
				{
					this._keys.Add(base.ReadBinary());
				}
			}

			// Token: 0x06000D38 RID: 3384 RVA: 0x00028E14 File Offset: 0x00027014
			protected override void SaveData()
			{
				base.WriteBinaryString(this._name);
				foreach (byte[] buffer in this._keys)
				{
					base.WriteBinaryString(buffer);
				}
			}

			// Token: 0x04000591 RID: 1425
			private byte[] _name;

			// Token: 0x04000592 RID: 1426
			private IList<byte[]> _keys;
		}

		// Token: 0x02000171 RID: 369
		private class SignatureKeyData : SshData
		{
			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x06000D39 RID: 3385 RVA: 0x00028E70 File Offset: 0x00027070
			// (set) Token: 0x06000D3A RID: 3386 RVA: 0x00028E78 File Offset: 0x00027078
			private byte[] AlgorithmName { get; set; }

			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x06000D3B RID: 3387 RVA: 0x00028E81 File Offset: 0x00027081
			// (set) Token: 0x06000D3C RID: 3388 RVA: 0x00028E89 File Offset: 0x00027089
			public byte[] Signature { get; private set; }

			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00028E92 File Offset: 0x00027092
			protected override int BufferCapacity
			{
				get
				{
					return base.BufferCapacity + 4 + this.AlgorithmName.Length + 4 + this.Signature.Length;
				}
			}

			// Token: 0x06000D3E RID: 3390 RVA: 0x00010840 File Offset: 0x0000EA40
			public SignatureKeyData()
			{
			}

			// Token: 0x06000D3F RID: 3391 RVA: 0x00028EB0 File Offset: 0x000270B0
			public SignatureKeyData(string name, byte[] signature)
			{
				this.AlgorithmName = SshData.Utf8.GetBytes(name);
				this.Signature = signature;
			}

			// Token: 0x06000D40 RID: 3392 RVA: 0x00028ED0 File Offset: 0x000270D0
			protected override void LoadData()
			{
				this.AlgorithmName = base.ReadBinary();
				this.Signature = base.ReadBinary();
			}

			// Token: 0x06000D41 RID: 3393 RVA: 0x00028EEA File Offset: 0x000270EA
			protected override void SaveData()
			{
				base.WriteBinaryString(this.AlgorithmName);
				base.WriteBinaryString(this.Signature);
			}
		}
	}
}
