using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x02000266 RID: 614
	internal sealed class ValidationState
	{
		// Token: 0x04000FE8 RID: 4072
		public bool IsNill;

		// Token: 0x04000FE9 RID: 4073
		public bool IsDefault;

		// Token: 0x04000FEA RID: 4074
		public bool NeedValidateChildren;

		// Token: 0x04000FEB RID: 4075
		public bool CheckRequiredAttribute;

		// Token: 0x04000FEC RID: 4076
		public bool ValidationSkipped;

		// Token: 0x04000FED RID: 4077
		public int Depth;

		// Token: 0x04000FEE RID: 4078
		public XmlSchemaContentProcessing ProcessContents;

		// Token: 0x04000FEF RID: 4079
		public XmlSchemaValidity Validity;

		// Token: 0x04000FF0 RID: 4080
		public SchemaElementDecl ElementDecl;

		// Token: 0x04000FF1 RID: 4081
		public SchemaElementDecl ElementDeclBeforeXsi;

		// Token: 0x04000FF2 RID: 4082
		public string LocalName;

		// Token: 0x04000FF3 RID: 4083
		public string Namespace;

		// Token: 0x04000FF4 RID: 4084
		public ConstraintStruct[] Constr;

		// Token: 0x04000FF5 RID: 4085
		public StateUnion CurrentState;

		// Token: 0x04000FF6 RID: 4086
		public bool HasMatched;

		// Token: 0x04000FF7 RID: 4087
		public BitSet[] CurPos = new BitSet[2];

		// Token: 0x04000FF8 RID: 4088
		public BitSet AllElementsSet;

		// Token: 0x04000FF9 RID: 4089
		public List<RangePositionInfo> RunningPositions;

		// Token: 0x04000FFA RID: 4090
		public bool TooComplex;
	}
}
