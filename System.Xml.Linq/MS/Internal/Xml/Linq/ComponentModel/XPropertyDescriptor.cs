using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace MS.Internal.Xml.Linq.ComponentModel
{
	// Token: 0x02000035 RID: 53
	internal abstract class XPropertyDescriptor<T, TProperty> : PropertyDescriptor where T : XObject
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		public XPropertyDescriptor(string name) : base(name, null)
		{
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000B9C2 File Offset: 0x00009BC2
		public override Type ComponentType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000B9CE File Offset: 0x00009BCE
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000B9D1 File Offset: 0x00009BD1
		public override Type PropertyType
		{
			get
			{
				return typeof(TProperty);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000B9DD File Offset: 0x00009BDD
		public override bool SupportsChangeEvents
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000B9E0 File Offset: 0x00009BE0
		public override void AddValueChanged(object component, EventHandler handler)
		{
			bool flag = base.GetValueChangedHandler(component) != null;
			base.AddValueChanged(component, handler);
			if (flag)
			{
				return;
			}
			T t = component as T;
			if (t != null && base.GetValueChangedHandler(component) != null)
			{
				t.Changing += this.OnChanging;
				t.Changed += this.OnChanged;
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000BA51 File Offset: 0x00009C51
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000BA54 File Offset: 0x00009C54
		public override void RemoveValueChanged(object component, EventHandler handler)
		{
			base.RemoveValueChanged(component, handler);
			T t = component as T;
			if (t != null && base.GetValueChangedHandler(component) == null)
			{
				t.Changing -= this.OnChanging;
				t.Changed -= this.OnChanged;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000BAB6 File Offset: 0x00009CB6
		public override void ResetValue(object component)
		{
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000BABA File Offset: 0x00009CBA
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000BABD File Offset: 0x00009CBD
		protected virtual void OnChanged(object sender, XObjectChangeEventArgs args)
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000BABF File Offset: 0x00009CBF
		protected virtual void OnChanging(object sender, XObjectChangeEventArgs args)
		{
		}
	}
}
