using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001FE RID: 510
	internal sealed class AllElementsContentValidator : ContentValidator
	{
		// Token: 0x0600210A RID: 8458 RVA: 0x000B478E File Offset: 0x000B298E
		public AllElementsContentValidator(XmlSchemaContentType contentType, int size, bool isEmptiable) : base(contentType, false, isEmptiable)
		{
			this.elements = new Hashtable(size);
			this.particles = new object[size];
			this.isRequired = new BitSet(size);
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000B47C0 File Offset: 0x000B29C0
		public bool AddElement(XmlQualifiedName name, object particle, bool isEmptiable)
		{
			if (this.elements[name] != null)
			{
				return false;
			}
			int count = this.elements.Count;
			this.elements.Add(name, count);
			this.particles[count] = particle;
			if (!isEmptiable)
			{
				this.isRequired.Set(count);
				this.countRequired++;
			}
			return true;
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x000B4822 File Offset: 0x000B2A22
		public override bool IsEmptiable
		{
			get
			{
				return base.IsEmptiable || this.countRequired == 0;
			}
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x000B4837 File Offset: 0x000B2A37
		public override void InitValidation(ValidationState context)
		{
			context.AllElementsSet = new BitSet(this.elements.Count);
			context.CurrentState.AllElementsRequired = -1;
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000B485C File Offset: 0x000B2A5C
		public override object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			object obj = this.elements[name];
			errorCode = 0;
			if (obj == null)
			{
				context.NeedValidateChildren = false;
				return null;
			}
			int num = (int)obj;
			if (context.AllElementsSet[num])
			{
				errorCode = -2;
				return null;
			}
			if (context.CurrentState.AllElementsRequired == -1)
			{
				context.CurrentState.AllElementsRequired = 0;
			}
			context.AllElementsSet.Set(num);
			if (this.isRequired[num])
			{
				context.CurrentState.AllElementsRequired = context.CurrentState.AllElementsRequired + 1;
			}
			return this.particles[num];
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x000B48EC File Offset: 0x000B2AEC
		public override bool CompleteValidation(ValidationState context)
		{
			return context.CurrentState.AllElementsRequired == this.countRequired || (this.IsEmptiable && context.CurrentState.AllElementsRequired == -1);
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000B491C File Offset: 0x000B2B1C
		public override ArrayList ExpectedElements(ValidationState context, bool isRequiredOnly)
		{
			ArrayList arrayList = null;
			foreach (object obj in this.elements)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!context.AllElementsSet[(int)dictionaryEntry.Value] && (!isRequiredOnly || this.isRequired[(int)dictionaryEntry.Value]))
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(dictionaryEntry.Key);
				}
			}
			return arrayList;
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x000B49C0 File Offset: 0x000B2BC0
		public override ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly, XmlSchemaSet schemaSet)
		{
			ArrayList result = new ArrayList();
			foreach (object obj in this.elements)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!context.AllElementsSet[(int)dictionaryEntry.Value] && (!isRequiredOnly || this.isRequired[(int)dictionaryEntry.Value]))
				{
					ContentValidator.AddParticleToExpected(this.particles[(int)dictionaryEntry.Value] as XmlSchemaParticle, schemaSet, result);
				}
			}
			return result;
		}

		// Token: 0x04000DE0 RID: 3552
		private Hashtable elements;

		// Token: 0x04000DE1 RID: 3553
		private object[] particles;

		// Token: 0x04000DE2 RID: 3554
		private BitSet isRequired;

		// Token: 0x04000DE3 RID: 3555
		private int countRequired;
	}
}
