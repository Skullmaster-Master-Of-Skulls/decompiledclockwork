using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001FA RID: 506
	internal sealed class DfaContentValidator : ContentValidator
	{
		// Token: 0x060020F8 RID: 8440 RVA: 0x000B3C6A File Offset: 0x000B1E6A
		internal DfaContentValidator(int[][] transitionTable, SymbolsDictionary symbols, XmlSchemaContentType contentType, bool isOpen, bool isEmptiable) : base(contentType, isOpen, isEmptiable)
		{
			this.transitionTable = transitionTable;
			this.symbols = symbols;
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x000B3C85 File Offset: 0x000B1E85
		public override void InitValidation(ValidationState context)
		{
			context.CurrentState.State = 0;
			context.HasMatched = (this.transitionTable[0][this.symbols.Count] > 0);
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x000B3CB0 File Offset: 0x000B1EB0
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

		// Token: 0x060020FB RID: 8443 RVA: 0x000B3D43 File Offset: 0x000B1F43
		public override bool CompleteValidation(ValidationState context)
		{
			return context.HasMatched;
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x000B3D50 File Offset: 0x000B1F50
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

		// Token: 0x060020FD RID: 8445 RVA: 0x000B3DE8 File Offset: 0x000B1FE8
		public override ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly, XmlSchemaSet schemaSet)
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
						if (xmlSchemaParticle != null)
						{
							ContentValidator.AddParticleToExpected(xmlSchemaParticle, schemaSet, arrayList);
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x04000DD0 RID: 3536
		private int[][] transitionTable;

		// Token: 0x04000DD1 RID: 3537
		private SymbolsDictionary symbols;
	}
}
