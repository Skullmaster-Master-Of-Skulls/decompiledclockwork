using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001FB RID: 507
	internal sealed class NfaContentValidator : ContentValidator
	{
		// Token: 0x060020FE RID: 8446 RVA: 0x000B3E45 File Offset: 0x000B2045
		internal NfaContentValidator(BitSet firstpos, BitSet[] followpos, SymbolsDictionary symbols, Positions positions, int endMarkerPos, XmlSchemaContentType contentType, bool isOpen, bool isEmptiable) : base(contentType, isOpen, isEmptiable)
		{
			this.firstpos = firstpos;
			this.followpos = followpos;
			this.symbols = symbols;
			this.positions = positions;
			this.endMarkerPos = endMarkerPos;
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000B3E78 File Offset: 0x000B2078
		public override void InitValidation(ValidationState context)
		{
			context.CurPos[0] = this.firstpos.Clone();
			context.CurPos[1] = new BitSet(this.firstpos.Count);
			context.CurrentState.CurPosIndex = 0;
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000B3EB4 File Offset: 0x000B20B4
		public override object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			BitSet bitSet = context.CurPos[context.CurrentState.CurPosIndex];
			int num = (context.CurrentState.CurPosIndex + 1) % 2;
			BitSet bitSet2 = context.CurPos[num];
			bitSet2.Clear();
			int num2 = this.symbols[name];
			object result = null;
			errorCode = 0;
			for (int num3 = bitSet.NextSet(-1); num3 != -1; num3 = bitSet.NextSet(num3))
			{
				if (num2 == this.positions[num3].symbol)
				{
					bitSet2.Or(this.followpos[num3]);
					result = this.positions[num3].particle;
					break;
				}
			}
			if (!bitSet2.IsEmpty)
			{
				context.CurrentState.CurPosIndex = num;
				return result;
			}
			if (base.IsOpen && bitSet[this.endMarkerPos])
			{
				return null;
			}
			context.NeedValidateChildren = false;
			errorCode = -1;
			return null;
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000B3F94 File Offset: 0x000B2194
		public override bool CompleteValidation(ValidationState context)
		{
			return context.CurPos[context.CurrentState.CurPosIndex][this.endMarkerPos];
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x000B3FB8 File Offset: 0x000B21B8
		public override ArrayList ExpectedElements(ValidationState context, bool isRequiredOnly)
		{
			ArrayList arrayList = null;
			BitSet bitSet = context.CurPos[context.CurrentState.CurPosIndex];
			for (int num = bitSet.NextSet(-1); num != -1; num = bitSet.NextSet(num))
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)this.positions[num].particle;
				if (xmlSchemaParticle == null)
				{
					string text = this.symbols.NameOf(this.positions[num].symbol);
					if (text.Length != 0)
					{
						arrayList.Add(text);
					}
				}
				else
				{
					string nameString = xmlSchemaParticle.NameString;
					if (!arrayList.Contains(nameString))
					{
						arrayList.Add(nameString);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x000B4064 File Offset: 0x000B2264
		public override ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly, XmlSchemaSet schemaSet)
		{
			ArrayList arrayList = new ArrayList();
			BitSet bitSet = context.CurPos[context.CurrentState.CurPosIndex];
			for (int num = bitSet.NextSet(-1); num != -1; num = bitSet.NextSet(num))
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)this.positions[num].particle;
				if (xmlSchemaParticle != null)
				{
					ContentValidator.AddParticleToExpected(xmlSchemaParticle, schemaSet, arrayList);
				}
			}
			return arrayList;
		}

		// Token: 0x04000DD2 RID: 3538
		private BitSet firstpos;

		// Token: 0x04000DD3 RID: 3539
		private BitSet[] followpos;

		// Token: 0x04000DD4 RID: 3540
		private SymbolsDictionary symbols;

		// Token: 0x04000DD5 RID: 3541
		private Positions positions;

		// Token: 0x04000DD6 RID: 3542
		private int endMarkerPos;
	}
}
