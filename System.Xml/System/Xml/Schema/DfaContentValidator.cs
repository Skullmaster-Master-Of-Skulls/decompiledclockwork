using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001A2 RID: 418
	internal sealed class DfaContentValidator : ContentValidator
	{
		// Token: 0x060015A6 RID: 5542 RVA: 0x0005FD4A File Offset: 0x0005ED4A
		internal DfaContentValidator(int[][] transitionTable, SymbolsDictionary symbols, XmlSchemaContentType contentType, bool isOpen, bool isEmptiable) : base(contentType, isOpen, isEmptiable)
		{
			this.transitionTable = transitionTable;
			this.symbols = symbols;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0005FD65 File Offset: 0x0005ED65
		public override void InitValidation(ValidationState context)
		{
			context.CurrentState.State = 0;
			context.HasMatched = (this.transitionTable[0][this.symbols.Count] > 0);
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0005FD90 File Offset: 0x0005ED90
		public override object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			int num = this.symbols[name];
			int num2 = this.transitionTable[context.CurrentState.State][num];
			errorCode = 0;
			if (num2 != -1)
			{
				context.CurrentState.State = num2;
				context.HasMatched = (this.transitionTable[context.CurrentState.State][this.symbols.Count] > 0);
				return this.symbols.GetParticle(num);
			}
			if (base.IsOpen && context.HasMatched)
			{
				return null;
			}
			context.NeedValidateChildren = false;
			errorCode = -1;
			return null;
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0005FE23 File Offset: 0x0005EE23
		public override bool CompleteValidation(ValidationState context)
		{
			return context.HasMatched;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0005FE30 File Offset: 0x0005EE30
		public override ArrayList ExpectedElements(ValidationState context, bool isRequiredOnly)
		{
			ArrayList arrayList = null;
			int[] array = this.transitionTable[context.CurrentState.State];
			if (array != null)
			{
				for (int i = 0; i < array.Length - 1; i++)
				{
					if (array[i] != -1)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)this.symbols.GetParticle(i);
						if (xmlSchemaParticle == null)
						{
							string text = this.symbols.NameOf(i);
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
				}
			}
			return arrayList;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0005FEC8 File Offset: 0x0005EEC8
		public override ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly)
		{
			ArrayList arrayList = new ArrayList();
			int[] array = this.transitionTable[context.CurrentState.State];
			if (array != null)
			{
				for (int i = 0; i < array.Length - 1; i++)
				{
					if (array[i] != -1)
					{
						XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)this.symbols.GetParticle(i);
						if (xmlSchemaParticle != null && !arrayList.Contains(xmlSchemaParticle))
						{
							arrayList.Add(xmlSchemaParticle);
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x04000CDB RID: 3291
		private int[][] transitionTable;

		// Token: 0x04000CDC RID: 3292
		private SymbolsDictionary symbols;
	}
}
