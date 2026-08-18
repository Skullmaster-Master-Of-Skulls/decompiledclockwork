using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200049D RID: 1181
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public class ParameterCollection : StateManagedCollection
	{
		// Token: 0x1700112E RID: 4398
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

		// Token: 0x1700112F RID: 4399
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

		// Token: 0x140000DA RID: 218
		// (add) Token: 0x06003AD9 RID: 15065 RVA: 0x000BEEB6 File Offset: 0x000BD0B6
		// (remove) Token: 0x06003ADA RID: 15066 RVA: 0x000BEECF File Offset: 0x000BD0CF
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

		// Token: 0x06003ADB RID: 15067 RVA: 0x000A9CAD File Offset: 0x000A7EAD
		public int Add(Parameter parameter)
		{
			return ((IList)this).Add(parameter);
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x000BEEE8 File Offset: 0x000BD0E8
		public int Add(string name, string value)
		{
			return ((IList)this).Add(new Parameter(name, TypeCode.Empty, value));
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x000BEEF8 File Offset: 0x000BD0F8
		public int Add(string name, TypeCode type, string value)
		{
			return ((IList)this).Add(new Parameter(name, type, value));
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x000BEF08 File Offset: 0x000BD108
		public int Add(string name, DbType dbType, string value)
		{
			return ((IList)this).Add(new Parameter(name, dbType, value));
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x000BEF18 File Offset: 0x000BD118
		internal void CallOnParametersChanged()
		{
			this.OnParametersChanged(EventArgs.Empty);
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x00095DD0 File Offset: 0x00093FD0
		public bool Contains(Parameter parameter)
		{
			return ((IList)this).Contains(parameter);
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x000B7C0D File Offset: 0x000B5E0D
		public void CopyTo(Parameter[] parameterArray, int index)
		{
			base.CopyTo(parameterArray, index);
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x000BEF28 File Offset: 0x000BD128
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

		// Token: 0x06003AE3 RID: 15075 RVA: 0x000BEF8D File Offset: 0x000BD18D
		protected override Type[] GetKnownTypes()
		{
			return ParameterCollection.knownTypes;
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x000BEF94 File Offset: 0x000BD194
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

		// Token: 0x06003AE5 RID: 15077 RVA: 0x000BEFCC File Offset: 0x000BD1CC
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

		// Token: 0x06003AE6 RID: 15078 RVA: 0x00095E55 File Offset: 0x00094055
		public int IndexOf(Parameter parameter)
		{
			return ((IList)this).IndexOf(parameter);
		}

		// Token: 0x06003AE7 RID: 15079 RVA: 0x00095E5E File Offset: 0x0009405E
		public void Insert(int index, Parameter parameter)
		{
			((IList)this).Insert(index, parameter);
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x000BF06C File Offset: 0x000BD26C
		protected override void OnClearComplete()
		{
			base.OnClearComplete();
			this.OnParametersChanged(EventArgs.Empty);
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x000BF07F File Offset: 0x000BD27F
		protected override void OnInsert(int index, object value)
		{
			base.OnInsert(index, value);
			((Parameter)value).SetOwner(this);
		}

		// Token: 0x06003AEA RID: 15082 RVA: 0x000BF095 File Offset: 0x000BD295
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
			this.OnParametersChanged(EventArgs.Empty);
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x000BF0AA File Offset: 0x000BD2AA
		protected virtual void OnParametersChanged(EventArgs e)
		{
			if (this._parametersChangedHandler != null)
			{
				this._parametersChangedHandler(this, e);
			}
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x000BF0C1 File Offset: 0x000BD2C1
		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);
			((Parameter)value).SetOwner(null);
			this.OnParametersChanged(EventArgs.Empty);
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x000BF0E2 File Offset: 0x000BD2E2
		protected override void OnValidate(object o)
		{
			base.OnValidate(o);
			if (!(o is Parameter))
			{
				throw new ArgumentException(SR.GetString("ParameterCollection_NotParameter"), "o");
			}
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x00095F15 File Offset: 0x00094115
		public void Remove(Parameter parameter)
		{
			((IList)this).Remove(parameter);
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x00095F0C File Offset: 0x0009410C
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x000BF108 File Offset: 0x000BD308
		protected override void SetDirtyObject(object o)
		{
			((Parameter)o).SetDirty();
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x000BF118 File Offset: 0x000BD318
		public void UpdateValues(HttpContext context, Control control)
		{
			foreach (object obj in this)
			{
				Parameter parameter = (Parameter)obj;
				parameter.UpdateValue(context, control);
			}
		}

		// Token: 0x04002311 RID: 8977
		private EventHandler _parametersChangedHandler;

		// Token: 0x04002312 RID: 8978
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
