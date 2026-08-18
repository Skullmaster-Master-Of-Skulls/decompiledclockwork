using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x0200013B RID: 315
	internal struct ParametersEncoder
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0001D58D File Offset: 0x0001B78D
		public BlobBuilder Builder { get; }

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001D595 File Offset: 0x0001B795
		public ParametersEncoder(BlobBuilder builder, bool allowVarArgs)
		{
			this.Builder = builder;
			this._allowOptional = allowVarArgs;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001D5A5 File Offset: 0x0001B7A5
		public ParameterTypeEncoder AddParameter()
		{
			return new ParameterTypeEncoder(this.Builder);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001D5B2 File Offset: 0x0001B7B2
		public ParametersEncoder StartVarArgs()
		{
			if (!this._allowOptional)
			{
				throw new InvalidOperationException();
			}
			this.Builder.WriteByte(65);
			return new ParametersEncoder(this.Builder, false);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x000031EB File Offset: 0x000013EB
		public void EndParameters()
		{
		}

		// Token: 0x040008B2 RID: 2226
		private readonly bool _allowOptional;
	}
}
