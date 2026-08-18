using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001A6 RID: 422
	internal sealed class AllElementsContentValidator : ContentValidator
	{
		// Token: 0x060015B8 RID: 5560 RVA: 0x00060890 File Offset: 0x0005F890
		public AllElementsContentValidator(XmlSchemaContentType contentType, int size, bool isEmptiable) : base(contentType, false, isEmptiable)
		{
			this.elements = new Hashtable(size);
			this.particles = new object[size];
			this.isRequired = new BitSet(size);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x000608C0 File Offset: 0x0005F8C0
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

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x00060922 File Offset: 0x0005F922
		public override bool IsEmptiable
		{
			get
			{
				return base.IsEmptiable || this.countRequired == 0;
			}
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00060937 File Offset: 0x0005F937
		public override void InitValidation(ValidationState context)
		{
			context.AllElementsSet = new BitSet(this.elements.Count);
			context.CurrentState.AllElementsRequired = -1;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x0006095C File Offset: 0x0005F95C
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

		// Token: 0x060015BD RID: 5565 RVA: 0x000609EF File Offset: 0x0005F9EF
		public override bool CompleteValidation(ValidationState context)
		{
			return context.CurrentState.AllElementsRequired == this.countRequired || (this.IsEmptiable && context.CurrentState.AllElementsRequired == -1);
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00060A20 File Offset: 0x0005FA20
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

		// Token: 0x060015BF RID: 5567 RVA: 0x00060AC4 File Offset: 0x0005FAC4
		public override ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.elements)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!context.AllElementsSet[(int)dictionaryEntry.Value] && (!isRequiredOnly || this.isRequired[(int)dictionaryEntry.Value]))
				{
					arrayList.Add(this.particles[(int)dictionaryEntry.Value]);
				}
			}
			return arrayList;
		}

		// Token: 0x04000CEB RID: 3307
		private Hashtable elements;

		// Token: 0x04000CEC RID: 3308
		private object[] particles;

		// Token: 0x04000CED RID: 3309
		private BitSet isRequired;

		// Token: 0x04000CEE RID: 3310
		private int countRequired;
	}
}
