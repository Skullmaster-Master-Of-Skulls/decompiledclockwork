using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq.Expressions;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200076E RID: 1902
	internal abstract class InternalPropertyValues
	{
		// Token: 0x06005644 RID: 22084 RVA: 0x00175ED2 File Offset: 0x001740D2
		protected InternalPropertyValues(InternalContext internalContext, Type type, bool isEntityValues)
		{
			this._internalContext = internalContext;
			this._type = type;
			this._isEntityValues = isEntityValues;
		}

		// Token: 0x06005645 RID: 22085
		protected abstract IPropertyValuesItem GetItemImpl(string propertyName);

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06005646 RID: 22086
		public abstract ISet<string> PropertyNames { get; }

		// Token: 0x06005647 RID: 22087 RVA: 0x00175EF0 File Offset: 0x001740F0
		public object ToObject()
		{
			object obj = this.CreateObject();
			IDictionary<string, Action<object, object>> propertySetters = DbHelpers.GetPropertySetters(this._type);
			foreach (string text in this.PropertyNames)
			{
				object obj2 = this.GetItem(text).Value;
				InternalPropertyValues internalPropertyValues = obj2 as InternalPropertyValues;
				if (internalPropertyValues != null)
				{
					obj2 = internalPropertyValues.ToObject();
				}
				Action<object, object> action;
				if (propertySetters.TryGetValue(text, out action))
				{
					action(obj, obj2);
				}
			}
			return obj;
		}

		// Token: 0x06005648 RID: 22088 RVA: 0x00175F84 File Offset: 0x00174184
		private object CreateObject()
		{
			if (this._isEntityValues)
			{
				return this._internalContext.CreateObject(this._type);
			}
			Func<object> func;
			if (!InternalPropertyValues._nonEntityFactories.TryGetValue(this._type, out func))
			{
				NewExpression body = Expression.New(this._type.GetDeclaredConstructor(new Type[0]));
				func = Expression.Lambda<Func<object>>(body, null).Compile();
				InternalPropertyValues._nonEntityFactories.TryAdd(this._type, func);
			}
			return func();
		}

		// Token: 0x06005649 RID: 22089 RVA: 0x00175FFC File Offset: 0x001741FC
		public void SetValues(object value)
		{
			IDictionary<string, Func<object, object>> propertyGetters = DbHelpers.GetPropertyGetters(value.GetType());
			foreach (string text in this.PropertyNames)
			{
				Func<object, object> func;
				if (propertyGetters.TryGetValue(text, out func))
				{
					object obj = func(value);
					IPropertyValuesItem item = this.GetItem(text);
					if (obj == null && item.IsComplex)
					{
						throw Error.DbPropertyValues_ComplexObjectCannotBeNull(text, this._type.Name);
					}
					InternalPropertyValues internalPropertyValues = item.Value as InternalPropertyValues;
					if (internalPropertyValues == null)
					{
						this.SetValue(item, obj);
					}
					else
					{
						internalPropertyValues.SetValues(obj);
					}
				}
			}
		}

		// Token: 0x0600564A RID: 22090 RVA: 0x001760B4 File Offset: 0x001742B4
		public InternalPropertyValues Clone()
		{
			return new ClonedPropertyValues(this, null);
		}

		// Token: 0x0600564B RID: 22091 RVA: 0x001760C0 File Offset: 0x001742C0
		public void SetValues(InternalPropertyValues values)
		{
			if (!this._type.IsAssignableFrom(values.ObjectType))
			{
				throw Error.DbPropertyValues_AttemptToSetValuesFromWrongType(values.ObjectType.Name, this._type.Name);
			}
			foreach (string text in this.PropertyNames)
			{
				IPropertyValuesItem item = values.GetItem(text);
				if (item.Value == null && item.IsComplex)
				{
					throw Error.DbPropertyValues_NestedPropertyValuesNull(text, this._type.Name);
				}
				this[text] = item.Value;
			}
		}

		// Token: 0x17000EE0 RID: 3808
		public object this[string propertyName]
		{
			get
			{
				return this.GetItem(propertyName).Value;
			}
			set
			{
				DbPropertyValues dbPropertyValues = value as DbPropertyValues;
				if (dbPropertyValues != null)
				{
					value = dbPropertyValues.InternalPropertyValues;
				}
				IPropertyValuesItem item = this.GetItem(propertyName);
				InternalPropertyValues internalPropertyValues = item.Value as InternalPropertyValues;
				if (internalPropertyValues == null)
				{
					this.SetValue(item, value);
					return;
				}
				InternalPropertyValues internalPropertyValues2 = value as InternalPropertyValues;
				if (internalPropertyValues2 == null)
				{
					throw Error.DbPropertyValues_AttemptToSetNonValuesOnComplexProperty();
				}
				internalPropertyValues.SetValues(internalPropertyValues2);
			}
		}

		// Token: 0x0600564E RID: 22094 RVA: 0x001761D2 File Offset: 0x001743D2
		public IPropertyValuesItem GetItem(string propertyName)
		{
			if (!this.PropertyNames.Contains(propertyName))
			{
				throw Error.DbPropertyValues_PropertyDoesNotExist(propertyName, this._type.Name);
			}
			return this.GetItemImpl(propertyName);
		}

		// Token: 0x0600564F RID: 22095 RVA: 0x001761FC File Offset: 0x001743FC
		private void SetValue(IPropertyValuesItem item, object newValue)
		{
			if (!DbHelpers.PropertyValuesEqual(item.Value, newValue))
			{
				if (item.Value == null && item.IsComplex)
				{
					throw Error.DbPropertyValues_NestedPropertyValuesNull(item.Name, this._type.Name);
				}
				if (newValue != null && !item.Type.IsAssignableFrom(newValue.GetType()))
				{
					throw Error.DbPropertyValues_WrongTypeForAssignment(newValue.GetType().Name, item.Name, item.Type.Name, this._type.Name);
				}
				item.Value = newValue;
			}
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06005650 RID: 22096 RVA: 0x00176288 File Offset: 0x00174488
		public Type ObjectType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06005651 RID: 22097 RVA: 0x00176290 File Offset: 0x00174490
		public InternalContext InternalContext
		{
			get
			{
				return this._internalContext;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06005652 RID: 22098 RVA: 0x00176298 File Offset: 0x00174498
		public bool IsEntityValues
		{
			get
			{
				return this._isEntityValues;
			}
		}

		// Token: 0x040022F1 RID: 8945
		private static readonly ConcurrentDictionary<Type, Func<object>> _nonEntityFactories = new ConcurrentDictionary<Type, Func<object>>();

		// Token: 0x040022F2 RID: 8946
		private readonly InternalContext _internalContext;

		// Token: 0x040022F3 RID: 8947
		private readonly Type _type;

		// Token: 0x040022F4 RID: 8948
		private readonly bool _isEntityValues;
	}
}
