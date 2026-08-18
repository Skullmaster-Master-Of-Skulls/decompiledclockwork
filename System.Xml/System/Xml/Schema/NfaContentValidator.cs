using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001A3 RID: 419
	internal sealed class NfaContentValidator : ContentValidator
	{
		// Token: 0x060015AC RID: 5548 RVA: 0x0005FF2E File Offset: 0x0005EF2E
		internal NfaContentValidator(BitSet firstpos, BitSet[] followpos, SymbolsDictionary symbols, Positions positions, int endMarkerPos, XmlSchemaContentType contentType, bool isOpen, bool isEmptiable) : base(contentType, isOpen, isEmptiable)
		{
			this.firstpos = firstpos;
			this.followpos = followpos;
			this.symbols = symbols;
			this.positions = positions;
			this.endMarkerPos = endMarkerPos;
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0005FF61 File Offset: 0x0005EF61
		public override void InitValidation(ValidationState context)
		{
			context.CurPos[0] = this.firstpos.Clone();
			context.CurPos[1] = new BitSet(this.firstpos.Count);
			context.CurrentState.CurPosIndex = 0;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0005FF9C File Offset: 0x0005EF9C
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

		// Token: 0x060015AF RID: 5551 RVA: 0x0006007C File Offset: 0x0005F07C
		public override bool CompleteValidation(ValidationState context)
		{
			return context.CurPos[context.CurrentState.CurPosIndex][this.endMarkerPos];
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x000600A0 File Offset: 0x0005F0A0
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

		// Token: 0x060015B1 RID: 5553 RVA: 0x0006014C File Offset: 0x0005F14C
		public override ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly)
		{
			ArrayList arrayList = new ArrayList();
			BitSet bitSet = context.CurPos[context.CurrentState.CurPosIndex];
			for (int num = bitSet.NextSet(-1); num != -1; num = bitSet.NextSet(num))
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)this.positions[num].particle;
				if (xmlSchemaParticle != null && !arrayList.Contains(xmlSchemaParticle))
				{
					arrayList.Add(xmlSchemaParticle);
				}
			}
			return arrayList;
		}

		// Token: 0x04000CDD RID: 3293
		private BitSet firstpos;

		// Token: 0x04000CDE RID: 3294
		private BitSet[] followpos;

		// Token: 0x04000CDF RID: 3295
		private SymbolsDictionary symbols;

		// Token: 0x04000CE0 RID: 3296
		private Positions positions;

		// Token: 0x04000CE1 RID: 3297
		private int endMarkerPos;
	}
}
