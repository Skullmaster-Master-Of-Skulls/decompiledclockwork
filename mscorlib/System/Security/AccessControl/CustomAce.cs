using System;
using System.Globalization;

namespace System.Security.AccessControl
{
	// Token: 0x020008FC RID: 2300
	public sealed class CustomAce : GenericAce
	{
		// Token: 0x0600534C RID: 21324 RVA: 0x0012D4E0 File Offset: 0x0012C4E0
		public CustomAce(AceType type, AceFlags flags, byte[] opaque) : base(type, flags)
		{
			if (type <= AceType.SystemAlarmCallbackObject)
			{
				throw new ArgumentOutOfRangeException("type", Environment.GetResourceString("ArgumentOutOfRange_InvalidUserDefinedAceType"));
			}
			this.SetOpaque(opaque);
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x0600534D RID: 21325 RVA: 0x0012D50B File Offset: 0x0012C50B
		public int OpaqueLength
		{
			get
			{
				if (this._opaque == null)
				{
					return 0;
				}
				return this._opaque.Length;
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x0600534E RID: 21326 RVA: 0x0012D51F File Offset: 0x0012C51F
		public override int BinaryLength
		{
			get
			{
				return 4 + this.OpaqueLength;
			}
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x0012D529 File Offset: 0x0012C529
		public byte[] GetOpaque()
		{
			return this._opaque;
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x0012D534 File Offset: 0x0012C534
		public void SetOpaque(byte[] opaque)
		{
			if (opaque != null)
			{
				if (opaque.Length > CustomAce.MaxOpaqueLength)
				{
					throw new ArgumentOutOfRangeException("opaque", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLength"), new object[]
					{
						0,
						CustomAce.MaxOpaqueLength
					}));
				}
				if (opaque.Length % 4 != 0)
				{
					throw new ArgumentOutOfRangeException("opaque", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLengthMultiple"), new object[]
					{
						4
					}));
				}
			}
			this._opaque = opaque;
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x0012D5C9 File Offset: 0x0012C5C9
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			base.MarshalHeader(binaryForm, offset);
			offset += 4;
			if (this.OpaqueLength != 0)
			{
				if (this.OpaqueLength > CustomAce.MaxOpaqueLength)
				{
					throw new SystemException();
				}
				this.GetOpaque().CopyTo(binaryForm, offset);
			}
		}

		// Token: 0x04002B38 RID: 11064
		private byte[] _opaque;

		// Token: 0x04002B39 RID: 11065
		public static readonly int MaxOpaqueLength = 65531;
	}
}
