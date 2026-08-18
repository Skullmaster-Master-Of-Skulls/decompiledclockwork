using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Assemblies
{
	// Token: 0x0200085D RID: 2141
	[Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
	[ComVisible(true)]
	[Serializable]
	public struct AssemblyHash : ICloneable
	{
		// Token: 0x06004E5E RID: 20062 RVA: 0x0010FDF4 File Offset: 0x0010EDF4
		[Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		public AssemblyHash(byte[] value)
		{
			this._Algorithm = AssemblyHashAlgorithm.SHA1;
			this._Value = null;
			if (value != null)
			{
				int num = value.Length;
				this._Value = new byte[num];
				Array.Copy(value, this._Value, num);
			}
		}

		// Token: 0x06004E5F RID: 20063 RVA: 0x0010FE34 File Offset: 0x0010EE34
		[Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		public AssemblyHash(AssemblyHashAlgorithm algorithm, byte[] value)
		{
			this._Algorithm = algorithm;
			this._Value = null;
			if (value != null)
			{
				int num = value.Length;
				this._Value = new byte[num];
				Array.Copy(value, this._Value, num);
			}
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06004E60 RID: 20064 RVA: 0x0010FE6F File Offset: 0x0010EE6F
		// (set) Token: 0x06004E61 RID: 20065 RVA: 0x0010FE77 File Offset: 0x0010EE77
		[Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		public AssemblyHashAlgorithm Algorithm
		{
			get
			{
				return this._Algorithm;
			}
			set
			{
				this._Algorithm = value;
			}
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x0010FE80 File Offset: 0x0010EE80
		[Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		public byte[] GetValue()
		{
			return this._Value;
		}

		// Token: 0x06004E63 RID: 20067 RVA: 0x0010FE88 File Offset: 0x0010EE88
		[Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		public void SetValue(byte[] value)
		{
			this._Value = value;
		}

		// Token: 0x06004E64 RID: 20068 RVA: 0x0010FE91 File Offset: 0x0010EE91
		[Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		public object Clone()
		{
			return new AssemblyHash(this._Algorithm, this._Value);
		}

		// Token: 0x04002879 RID: 10361
		private AssemblyHashAlgorithm _Algorithm;

		// Token: 0x0400287A RID: 10362
		private byte[] _Value;

		// Token: 0x0400287B RID: 10363
		[Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static readonly AssemblyHash Empty = new AssemblyHash(AssemblyHashAlgorithm.None, null);
	}
}
