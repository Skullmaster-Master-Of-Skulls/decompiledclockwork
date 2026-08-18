using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200060F RID: 1551
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ParameterCollection : StateManagedCollection
	{
		// Token: 0x17001348 RID: 4936
		public Parameter this[int index]
		{
			get
			{
				return (Parameter)((IList)this)[index];
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		// Token: 0x17001349 RID: 4937
		public Parameter this[string name]
		{
			get
			{
				int parameterIndex = this.GetParameterIndex(name);
				if (parameterIndex == -1)
				{
					return null;
				}
				return this[parameterIndex];
			}
			set
			{
				int parameterIndex = this.GetParameterIndex(name);
				if (parameterIndex == -1)
				{
					this.Add(value);
					return;
				}
				this[parameterIndex] = value;
			}
		}

		// Token: 0x140000F2 RID: 242
		// (add) Token: 0x06004CAC RID: 19628 RVA: 0x0013739E File Offset: 0x0013639E
		// (remove) Token: 0x06004CAD RID: 19629 RVA: 0x001373B7 File Offset: 0x001363B7
		public event EventHandler ParametersChanged
		{
			add
			{
				this._parametersChangedHandler = (EventHandler)Delegate.Combine(this._parametersChangedHandler, value);
			}
			remove
			{
				this._parametersChangedHandler = (EventHandler)Delegate.Remove(this._parametersChangedHandler, value);
			}
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x001373D0 File Offset: 0x001363D0
		public int Add(Parameter parameter)
		{
			return ((IList)this).Add(parameter);
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x001373D9 File Offset: 0x001363D9
		public int Add(string name, string value)
		{
			return ((IList)this).Add(new Parameter(name, TypeCode.Empty, value));
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x001373E9 File Offset: 0x001363E9
		public int Add(string name, TypeCode type, string value)
		{
			return ((IList)this).Add(new Parameter(name, type, value));
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x001373F9 File Offset: 0x001363F9
		public int Add(string name, DbType dbType, string value)
		{
			return ((IList)this).Add(new Parameter(name, dbType, value));
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x00137409 File Offset: 0x00136409
		internal void CallOnParametersChanged()
		{
			this.OnParametersChanged(EventArgs.Empty);
		}

		// Token: 0x06004CB3 RID: 19635 RVA: 0x00137416 File Offset: 0x00136416
		public bool Contains(Parameter parameter)
		{
			return ((IList)this).Contains(parameter);
		}

		// Token: 0x06004CB4 RID: 19636 RVA: 0x0013741F File Offset: 0x0013641F
		public void CopyTo(Parameter[] parameterArray, int index)
		{
			base.CopyTo(parameterArray, index);
		}

		// Token: 0x06004CB5 RID: 19637 RVA: 0x0013742C File Offset: 0x0013642C
		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
			case 0:
				return new ControlParameter();
			case 1:
				return new CookieParameter();
			case 2:
				return new FormParameter();
			case 3:
				return new Parameter();
			case 4:
				return new QueryStringParameter();
			case 5:
				return new SessionParameter();
			case 6:
				return new ProfileParameter();
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x06004CB6 RID: 19638 RVA: 0x00137493 File Offset: 0x00136493
		protected override Type[] GetKnownTypes()
		{
			return ParameterCollection.knownTypes;
		}

		// Token: 0x06004CB7 RID: 19639 RVA: 0x0013749C File Offset: 0x0013649C
		private int GetParameterIndex(string name)
		{
			for (int i = 0; i < base.Count; i++)
			{
				if (string.Equals(this[i].Name, name, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06004CB8 RID: 19640 RVA: 0x001374D4 File Offset: 0x001364D4
		public IOrderedDictionary GetValues(HttpContext context, Control control)
		{
			this.UpdateValues(context, control);
			IOrderedDictionary orderedDictionary = new OrderedDictionary();
			foreach (object obj in this)
			{
				Parameter parameter = (Parameter)obj;
				string key = parameter.Name;
				int num = 1;
				while (orderedDictionary.Contains(key))
				{
					key = parameter.Name + num.ToString(CultureInfo.InvariantCulture);
					num++;
				}
				orderedDictionary.Add(key, parameter.ParameterValue);
			}
			return orderedDictionary;
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x00137574 File Offset: 0x00136574
		public int IndexOf(Parameter parameter)
		{
			return ((IList)this).IndexOf(parameter);
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x0013757D File Offset: 0x0013657D
		public void Insert(int index, Parameter parameter)
		{
			((IList)this).Insert(index, parameter);
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x00137587 File Offset: 0x00136587
		protected override void OnClearComplete()
		{
			base.OnClearComplete();
			this.OnParametersChanged(EventArgs.Empty);
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x0013759A File Offset: 0x0013659A
		protected override void OnInsert(int index, object value)
		{
			base.OnInsert(index, value);
			((Parameter)value).SetOwner(this);
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x001375B0 File Offset: 0x001365B0
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
			this.OnParametersChanged(EventArgs.Empty);
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x001375C5 File Offset: 0x001365C5
		protected virtual void OnParametersChanged(EventArgs e)
		{
			if (this._parametersChangedHandler != null)
			{
				this._parametersChangedHandler(this, e);
			}
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x001375DC File Offset: 0x001365DC
		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);
			((Parameter)value).SetOwner(null);
			this.OnParametersChanged(EventArgs.Empty);
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x001375FD File Offset: 0x001365FD
		protected override void OnValidate(object o)
		{
			base.OnValidate(o);
			if (!(o is Parameter))
			{
				throw new ArgumentException(SR.GetString("ParameterCollection_NotParameter"), "o");
			}
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x00137623 File Offset: 0x00136623
		public void Remove(Parameter parameter)
		{
			((IList)this).Remove(parameter);
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x0013762C File Offset: 0x0013662C
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x00137635 File Offset: 0x00136635
		protected override void SetDirtyObject(object o)
		{
			((Parameter)o).SetDirty();
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x00137644 File Offset: 0x00136644
		public void UpdateValues(HttpContext context, Control control)
		{
			foreach (object obj in this)
			{
				Parameter parameter = (Parameter)obj;
				parameter.UpdateValue(context, control);
			}
		}

		// Token: 0x04002C14 RID: 11284
		private EventHandler _parametersChangedHandler;

		// Token: 0x04002C15 RID: 11285
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(ControlParameter),
			typeof(CookieParameter),
			typeof(FormParameter),
			typeof(Parameter),
			typeof(QueryStringParameter),
			typeof(SessionParameter),
			typeof(ProfileParameter)
		};
	}
}
