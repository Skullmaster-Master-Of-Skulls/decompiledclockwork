using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004B4 RID: 1204
	internal class ComNativeDescriptor : TypeDescriptionProvider
	{
		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06004F63 RID: 20323 RVA: 0x00147AAC File Offset: 0x00145CAC
		internal static ComNativeDescriptor Instance
		{
			get
			{
				if (ComNativeDescriptor.handler == null)
				{
					ComNativeDescriptor.handler = new ComNativeDescriptor();
				}
				return ComNativeDescriptor.handler;
			}
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x00147AC4 File Offset: 0x00145CC4
		public static object GetNativePropertyValue(object component, string propertyName, ref bool succeeded)
		{
			return ComNativeDescriptor.Instance.GetPropertyValue(component, propertyName, ref succeeded);
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x00147AD3 File Offset: 0x00145CD3
		public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			return new ComNativeDescriptor.ComTypeDescriptor(this, instance);
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x00147ADC File Offset: 0x00145CDC
		internal string GetClassName(object component)
		{
			string text = null;
			if (component is NativeMethods.IVsPerPropertyBrowsing)
			{
				int className = ((NativeMethods.IVsPerPropertyBrowsing)component).GetClassName(ref text);
				if (NativeMethods.Succeeded(className) && text != null)
				{
					return text;
				}
			}
			UnsafeNativeMethods.ITypeInfo typeInfo = Com2TypeInfoProcessor.FindTypeInfo(component, true);
			if (typeInfo == null)
			{
				return "";
			}
			if (typeInfo != null)
			{
				string text2 = null;
				try
				{
					typeInfo.GetDocumentation(-1, ref text, ref text2, null, null);
					while (text != null && text.Length > 0 && text[0] == '_')
					{
						text = text.Substring(1);
					}
					return text;
				}
				catch
				{
				}
			}
			return "";
		}

		// Token: 0x06004F67 RID: 20327 RVA: 0x00147B74 File Offset: 0x00145D74
		internal TypeConverter GetConverter(object component)
		{
			return TypeDescriptor.GetConverter(typeof(IComponent));
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x00147B85 File Offset: 0x00145D85
		internal object GetEditor(object component, Type baseEditorType)
		{
			return TypeDescriptor.GetEditor(component.GetType(), baseEditorType);
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x00147B94 File Offset: 0x00145D94
		internal string GetName(object component)
		{
			if (!(component is UnsafeNativeMethods.IDispatch))
			{
				return "";
			}
			int nameDispId = Com2TypeInfoProcessor.GetNameDispId((UnsafeNativeMethods.IDispatch)component);
			if (nameDispId != -1)
			{
				bool flag = false;
				object propertyValue = this.GetPropertyValue(component, nameDispId, ref flag);
				if (flag && propertyValue != null)
				{
					return propertyValue.ToString();
				}
			}
			return "";
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x00147BE0 File Offset: 0x00145DE0
		internal object GetPropertyValue(object component, string propertyName, ref bool succeeded)
		{
			if (!(component is UnsafeNativeMethods.IDispatch))
			{
				return null;
			}
			UnsafeNativeMethods.IDispatch dispatch = (UnsafeNativeMethods.IDispatch)component;
			string[] rgszNames = new string[]
			{
				propertyName
			};
			int[] array = new int[]
			{
				-1
			};
			Guid empty = Guid.Empty;
			try
			{
				int idsOfNames = dispatch.GetIDsOfNames(ref empty, rgszNames, 1, SafeNativeMethods.GetThreadLCID(), array);
				if (array[0] == -1 || NativeMethods.Failed(idsOfNames))
				{
					return null;
				}
			}
			catch
			{
				return null;
			}
			return this.GetPropertyValue(component, array[0], ref succeeded);
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x00147C68 File Offset: 0x00145E68
		internal object GetPropertyValue(object component, int dispid, ref bool succeeded)
		{
			if (!(component is UnsafeNativeMethods.IDispatch))
			{
				return null;
			}
			object[] array = new object[1];
			if (this.GetPropertyValue(component, dispid, array) == 0)
			{
				succeeded = true;
				return array[0];
			}
			succeeded = false;
			return null;
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x00147C9C File Offset: 0x00145E9C
		internal int GetPropertyValue(object component, int dispid, object[] retval)
		{
			if (!(component is UnsafeNativeMethods.IDispatch))
			{
				return -2147467262;
			}
			UnsafeNativeMethods.IDispatch dispatch = (UnsafeNativeMethods.IDispatch)component;
			try
			{
				Guid empty = Guid.Empty;
				NativeMethods.tagEXCEPINFO tagEXCEPINFO = new NativeMethods.tagEXCEPINFO();
				int num;
				try
				{
					num = dispatch.Invoke(dispid, ref empty, SafeNativeMethods.GetThreadLCID(), 2, new NativeMethods.tagDISPPARAMS(), retval, tagEXCEPINFO, null);
					if (num == -2147352567)
					{
						num = tagEXCEPINFO.scode;
					}
				}
				catch (ExternalException ex)
				{
					num = ex.ErrorCode;
				}
				return num;
			}
			catch
			{
			}
			return -2147467259;
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x00147D2C File Offset: 0x00145F2C
		internal bool IsNameDispId(object obj, int dispid)
		{
			return obj != null && obj.GetType().IsCOMObject && dispid == Com2TypeInfoProcessor.GetNameDispId((UnsafeNativeMethods.IDispatch)obj);
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x00147D50 File Offset: 0x00145F50
		private void CheckClear(object component)
		{
			int num = this.clearCount + 1;
			this.clearCount = num;
			if (num % 25 == 0)
			{
				WeakHashtable obj = this.nativeProps;
				lock (obj)
				{
					this.clearCount = 0;
					List<object> list = null;
					foreach (object obj2 in this.nativeProps)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
						Com2Properties com2Properties = dictionaryEntry.Value as Com2Properties;
						if (com2Properties != null && com2Properties.TooOld)
						{
							if (list == null)
							{
								list = new List<object>(3);
							}
							list.Add(dictionaryEntry.Key);
						}
					}
					if (list != null)
					{
						for (int i = list.Count - 1; i >= 0; i--)
						{
							object key = list[i];
							Com2Properties com2Properties = this.nativeProps[key] as Com2Properties;
							if (com2Properties != null)
							{
								com2Properties.Disposed -= this.OnPropsInfoDisposed;
								com2Properties.Dispose();
								this.nativeProps.Remove(key);
							}
						}
					}
				}
			}
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x00147E8C File Offset: 0x0014608C
		private Com2Properties GetPropsInfo(object component)
		{
			this.CheckClear(component);
			Com2Properties com2Properties = (Com2Properties)this.nativeProps[component];
			if (com2Properties == null || !com2Properties.CheckValid())
			{
				com2Properties = Com2TypeInfoProcessor.GetProperties(component);
				if (com2Properties != null)
				{
					com2Properties.Disposed += this.OnPropsInfoDisposed;
					this.nativeProps.SetWeak(component, com2Properties);
					com2Properties.AddExtendedBrowsingHandlers(this.extendedBrowsingHandlers);
				}
			}
			return com2Properties;
		}

		// Token: 0x06004F70 RID: 20336 RVA: 0x00147EF4 File Offset: 0x001460F4
		internal AttributeCollection GetAttributes(object component)
		{
			ArrayList arrayList = new ArrayList();
			if (component is NativeMethods.IManagedPerPropertyBrowsing)
			{
				object[] componentAttributes = Com2IManagedPerPropertyBrowsingHandler.GetComponentAttributes((NativeMethods.IManagedPerPropertyBrowsing)component, -1);
				object[] array = componentAttributes;
				for (int i = 0; i < array.Length; i++)
				{
					arrayList.Add(array[i]);
				}
			}
			if (Com2ComponentEditor.NeedsComponentEditor(component))
			{
				EditorAttribute value = new EditorAttribute(typeof(Com2ComponentEditor), typeof(ComponentEditor));
				arrayList.Add(value);
			}
			if (arrayList == null || arrayList.Count == 0)
			{
				return this.staticAttrs;
			}
			Attribute[] array2 = new Attribute[arrayList.Count];
			arrayList.CopyTo(array2, 0);
			return new AttributeCollection(array2);
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x00147F94 File Offset: 0x00146194
		internal PropertyDescriptor GetDefaultProperty(object component)
		{
			this.CheckClear(component);
			Com2Properties propsInfo = this.GetPropsInfo(component);
			if (propsInfo != null)
			{
				return propsInfo.DefaultProperty;
			}
			return null;
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x00147FBB File Offset: 0x001461BB
		internal EventDescriptorCollection GetEvents(object component)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06004F73 RID: 20339 RVA: 0x00147FBB File Offset: 0x001461BB
		internal EventDescriptorCollection GetEvents(object component, Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06004F74 RID: 20340 RVA: 0x00015ECC File Offset: 0x000140CC
		internal EventDescriptor GetDefaultEvent(object component)
		{
			return null;
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x00147FC4 File Offset: 0x001461C4
		internal PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
		{
			Com2Properties propsInfo = this.GetPropsInfo(component);
			if (propsInfo == null)
			{
				return PropertyDescriptorCollection.Empty;
			}
			PropertyDescriptorCollection result;
			try
			{
				propsInfo.AlwaysValid = true;
				PropertyDescriptor[] properties = propsInfo.Properties;
				PropertyDescriptor[] properties2 = properties;
				result = new PropertyDescriptorCollection(properties2);
			}
			finally
			{
				propsInfo.AlwaysValid = false;
			}
			return result;
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x00148018 File Offset: 0x00146218
		private void OnPropsInfoDisposed(object sender, EventArgs e)
		{
			Com2Properties com2Properties = sender as Com2Properties;
			if (com2Properties != null)
			{
				com2Properties.Disposed -= this.OnPropsInfoDisposed;
				WeakHashtable obj = this.nativeProps;
				lock (obj)
				{
					object obj2 = com2Properties.TargetObject;
					if (obj2 == null && this.nativeProps.ContainsValue(com2Properties))
					{
						foreach (object obj3 in this.nativeProps)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
							if (dictionaryEntry.Value == com2Properties)
							{
								obj2 = dictionaryEntry.Key;
								break;
							}
						}
						if (obj2 == null)
						{
							return;
						}
					}
					this.nativeProps.Remove(obj2);
				}
			}
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x001480F8 File Offset: 0x001462F8
		internal static void ResolveVariantTypeConverterAndTypeEditor(object propertyValue, ref TypeConverter currentConverter, Type editorType, ref object currentEditor)
		{
			if (propertyValue != null && propertyValue != null && !Convert.IsDBNull(propertyValue))
			{
				Type type = propertyValue.GetType();
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				if (converter != null && converter.GetType() != typeof(TypeConverter))
				{
					currentConverter = converter;
				}
				object editor = TypeDescriptor.GetEditor(type, editorType);
				if (editor != null)
				{
					currentEditor = editor;
				}
			}
		}

		// Token: 0x04003467 RID: 13415
		private static ComNativeDescriptor handler;

		// Token: 0x04003468 RID: 13416
		private AttributeCollection staticAttrs = new AttributeCollection(new Attribute[]
		{
			BrowsableAttribute.Yes,
			DesignTimeVisibleAttribute.No
		});

		// Token: 0x04003469 RID: 13417
		private WeakHashtable nativeProps = new WeakHashtable();

		// Token: 0x0400346A RID: 13418
		private Hashtable extendedBrowsingHandlers = new Hashtable();

		// Token: 0x0400346B RID: 13419
		private int clearCount;

		// Token: 0x0400346C RID: 13420
		private const int CLEAR_INTERVAL = 25;

		// Token: 0x02000858 RID: 2136
		private sealed class ComTypeDescriptor : ICustomTypeDescriptor
		{
			// Token: 0x060070BE RID: 28862 RVA: 0x0019D360 File Offset: 0x0019B560
			internal ComTypeDescriptor(ComNativeDescriptor handler, object instance)
			{
				this._handler = handler;
				this._instance = instance;
			}

			// Token: 0x060070BF RID: 28863 RVA: 0x0019D376 File Offset: 0x0019B576
			AttributeCollection ICustomTypeDescriptor.GetAttributes()
			{
				return this._handler.GetAttributes(this._instance);
			}

			// Token: 0x060070C0 RID: 28864 RVA: 0x0019D389 File Offset: 0x0019B589
			string ICustomTypeDescriptor.GetClassName()
			{
				return this._handler.GetClassName(this._instance);
			}

			// Token: 0x060070C1 RID: 28865 RVA: 0x0019D39C File Offset: 0x0019B59C
			string ICustomTypeDescriptor.GetComponentName()
			{
				return this._handler.GetName(this._instance);
			}

			// Token: 0x060070C2 RID: 28866 RVA: 0x0019D3AF File Offset: 0x0019B5AF
			TypeConverter ICustomTypeDescriptor.GetConverter()
			{
				return this._handler.GetConverter(this._instance);
			}

			// Token: 0x060070C3 RID: 28867 RVA: 0x0019D3C2 File Offset: 0x0019B5C2
			EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
			{
				return this._handler.GetDefaultEvent(this._instance);
			}

			// Token: 0x060070C4 RID: 28868 RVA: 0x0019D3D5 File Offset: 0x0019B5D5
			PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
			{
				return this._handler.GetDefaultProperty(this._instance);
			}

			// Token: 0x060070C5 RID: 28869 RVA: 0x0019D3E8 File Offset: 0x0019B5E8
			object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
			{
				return this._handler.GetEditor(this._instance, editorBaseType);
			}

			// Token: 0x060070C6 RID: 28870 RVA: 0x0019D3FC File Offset: 0x0019B5FC
			EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
			{
				return this._handler.GetEvents(this._instance);
			}

			// Token: 0x060070C7 RID: 28871 RVA: 0x0019D40F File Offset: 0x0019B60F
			EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
			{
				return this._handler.GetEvents(this._instance, attributes);
			}

			// Token: 0x060070C8 RID: 28872 RVA: 0x0019D423 File Offset: 0x0019B623
			PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
			{
				return this._handler.GetProperties(this._instance, null);
			}

			// Token: 0x060070C9 RID: 28873 RVA: 0x0019D437 File Offset: 0x0019B637
			PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
			{
				return this._handler.GetProperties(this._instance, attributes);
			}

			// Token: 0x060070CA RID: 28874 RVA: 0x0019D44B File Offset: 0x0019B64B
			object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
			{
				return this._instance;
			}

			// Token: 0x040043AA RID: 17322
			private ComNativeDescriptor _handler;

			// Token: 0x040043AB RID: 17323
			private object _instance;
		}
	}
}
