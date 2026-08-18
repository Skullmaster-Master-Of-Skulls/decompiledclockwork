using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x0200049E RID: 1182
	[SuppressUnmanagedCodeSecurity]
	internal class Com2IPerPropertyBrowsingHandler : Com2ExtendedBrowsingHandler
	{
		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x06004EAE RID: 20142 RVA: 0x00143DB4 File Offset: 0x00141FB4
		public override Type Interface
		{
			get
			{
				return typeof(NativeMethods.IPerPropertyBrowsing);
			}
		}

		// Token: 0x06004EAF RID: 20143 RVA: 0x00143DC0 File Offset: 0x00141FC0
		public override void SetupPropertyHandlers(Com2PropertyDescriptor[] propDesc)
		{
			if (propDesc == null)
			{
				return;
			}
			for (int i = 0; i < propDesc.Length; i++)
			{
				propDesc[i].QueryGetBaseAttributes += this.OnGetBaseAttributes;
				propDesc[i].QueryGetDisplayValue += this.OnGetDisplayValue;
				propDesc[i].QueryGetTypeConverterAndTypeEditor += this.OnGetTypeConverterAndTypeEditor;
			}
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x00143E1C File Offset: 0x0014201C
		private Guid GetPropertyPageGuid(NativeMethods.IPerPropertyBrowsing target, int dispid)
		{
			Guid result;
			if (target.MapPropertyToPage(dispid, out result) == 0)
			{
				return result;
			}
			return Guid.Empty;
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x00143E40 File Offset: 0x00142040
		internal static string GetDisplayString(NativeMethods.IPerPropertyBrowsing ppb, int dispid, ref bool success)
		{
			string[] array = new string[1];
			if (ppb.GetDisplayString(dispid, array) == 0)
			{
				success = (array[0] != null);
				return array[0];
			}
			success = false;
			return null;
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x00143E70 File Offset: 0x00142070
		private void OnGetBaseAttributes(Com2PropertyDescriptor sender, GetAttributesEvent attrEvent)
		{
			NativeMethods.IPerPropertyBrowsing perPropertyBrowsing = sender.TargetObject as NativeMethods.IPerPropertyBrowsing;
			if (perPropertyBrowsing != null)
			{
				bool flag = !Guid.Empty.Equals(this.GetPropertyPageGuid(perPropertyBrowsing, sender.DISPID));
				if (sender.CanShow && flag && typeof(UnsafeNativeMethods.IDispatch).IsAssignableFrom(sender.PropertyType))
				{
					attrEvent.Add(BrowsableAttribute.Yes);
				}
			}
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x00143ED8 File Offset: 0x001420D8
		private void OnGetDisplayValue(Com2PropertyDescriptor sender, GetNameItemEvent gnievent)
		{
			try
			{
				if (sender.TargetObject is NativeMethods.IPerPropertyBrowsing)
				{
					if (!(sender.Converter is Com2IPerPropertyBrowsingHandler.Com2IPerPropertyEnumConverter) && !sender.ConvertingNativeType)
					{
						bool flag = true;
						string displayString = Com2IPerPropertyBrowsingHandler.GetDisplayString((NativeMethods.IPerPropertyBrowsing)sender.TargetObject, sender.DISPID, ref flag);
						if (flag)
						{
							gnievent.Name = displayString;
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x00143F44 File Offset: 0x00142144
		private void OnGetTypeConverterAndTypeEditor(Com2PropertyDescriptor sender, GetTypeConverterAndTypeEditorEvent gveevent)
		{
			if (sender.TargetObject is NativeMethods.IPerPropertyBrowsing)
			{
				NativeMethods.IPerPropertyBrowsing perPropertyBrowsing = (NativeMethods.IPerPropertyBrowsing)sender.TargetObject;
				NativeMethods.CA_STRUCT ca_STRUCT = new NativeMethods.CA_STRUCT();
				NativeMethods.CA_STRUCT ca_STRUCT2 = new NativeMethods.CA_STRUCT();
				int num = 0;
				try
				{
					num = perPropertyBrowsing.GetPredefinedStrings(sender.DISPID, ca_STRUCT, ca_STRUCT2);
				}
				catch (ExternalException ex)
				{
					num = ex.ErrorCode;
				}
				if (gveevent.TypeConverter is Com2IPerPropertyBrowsingHandler.Com2IPerPropertyEnumConverter)
				{
					gveevent.TypeConverter = null;
				}
				bool flag = num == 0;
				if (flag)
				{
					OleStrCAMarshaler oleStrCAMarshaler = new OleStrCAMarshaler(ca_STRUCT);
					Int32CAMarshaler int32CAMarshaler = new Int32CAMarshaler(ca_STRUCT2);
					if (oleStrCAMarshaler.Count > 0 && int32CAMarshaler.Count > 0)
					{
						gveevent.TypeConverter = new Com2IPerPropertyBrowsingHandler.Com2IPerPropertyEnumConverter(new Com2IPerPropertyBrowsingHandler.Com2IPerPropertyBrowsingEnum(sender, this, oleStrCAMarshaler, int32CAMarshaler, true));
					}
				}
				if (!flag)
				{
					if (sender.ConvertingNativeType)
					{
						return;
					}
					Guid propertyPageGuid = this.GetPropertyPageGuid(perPropertyBrowsing, sender.DISPID);
					if (!Guid.Empty.Equals(propertyPageGuid))
					{
						gveevent.TypeEditor = new Com2PropertyPageUITypeEditor(sender, propertyPageGuid, (UITypeEditor)gveevent.TypeEditor);
					}
				}
			}
		}

		// Token: 0x02000852 RID: 2130
		private class Com2IPerPropertyEnumConverter : Com2EnumConverter
		{
			// Token: 0x06007096 RID: 28822 RVA: 0x0019CCF1 File Offset: 0x0019AEF1
			public Com2IPerPropertyEnumConverter(Com2IPerPropertyBrowsingHandler.Com2IPerPropertyBrowsingEnum items) : base(items)
			{
				this.itemsEnum = items;
			}

			// Token: 0x06007097 RID: 28823 RVA: 0x0019CD04 File Offset: 0x0019AF04
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
			{
				if (destType == typeof(string) && !this.itemsEnum.arraysFetched)
				{
					object value2 = this.itemsEnum.target.GetValue(this.itemsEnum.target.TargetObject);
					if (value2 == value || (value2 != null && value2.Equals(value)))
					{
						bool flag = false;
						string displayString = Com2IPerPropertyBrowsingHandler.GetDisplayString((NativeMethods.IPerPropertyBrowsing)this.itemsEnum.target.TargetObject, this.itemsEnum.target.DISPID, ref flag);
						if (flag)
						{
							return displayString;
						}
					}
				}
				return base.ConvertTo(context, culture, value, destType);
			}

			// Token: 0x0400438E RID: 17294
			private Com2IPerPropertyBrowsingHandler.Com2IPerPropertyBrowsingEnum itemsEnum;
		}

		// Token: 0x02000853 RID: 2131
		private class Com2IPerPropertyBrowsingEnum : Com2Enum
		{
			// Token: 0x06007098 RID: 28824 RVA: 0x0019CDA1 File Offset: 0x0019AFA1
			public Com2IPerPropertyBrowsingEnum(Com2PropertyDescriptor targetObject, Com2IPerPropertyBrowsingHandler handler, OleStrCAMarshaler names, Int32CAMarshaler values, bool allowUnknowns) : base(new string[0], new object[0], allowUnknowns)
			{
				this.target = targetObject;
				this.nameMarshaller = names;
				this.valueMarshaller = values;
				this.handler = handler;
				this.arraysFetched = false;
			}

			// Token: 0x17001888 RID: 6280
			// (get) Token: 0x06007099 RID: 28825 RVA: 0x0019CDDB File Offset: 0x0019AFDB
			public override object[] Values
			{
				get
				{
					this.EnsureArrays();
					return base.Values;
				}
			}

			// Token: 0x17001889 RID: 6281
			// (get) Token: 0x0600709A RID: 28826 RVA: 0x0019CDE9 File Offset: 0x0019AFE9
			public override string[] Names
			{
				get
				{
					this.EnsureArrays();
					return base.Names;
				}
			}

			// Token: 0x0600709B RID: 28827 RVA: 0x0019CDF8 File Offset: 0x0019AFF8
			private void EnsureArrays()
			{
				if (this.arraysFetched)
				{
					return;
				}
				this.arraysFetched = true;
				try
				{
					object[] items = this.nameMarshaller.Items;
					object[] items2 = this.valueMarshaller.Items;
					NativeMethods.IPerPropertyBrowsing perPropertyBrowsing = (NativeMethods.IPerPropertyBrowsing)this.target.TargetObject;
					int num = 0;
					if (items.Length != 0)
					{
						object[] array = new object[items2.Length];
						NativeMethods.VARIANT variant = new NativeMethods.VARIANT();
						Type propertyType = this.target.PropertyType;
						for (int i = items.Length - 1; i >= 0; i--)
						{
							int dwCookie = (int)items2[i];
							if (items[i] != null && items[i] is string)
							{
								variant.vt = 0;
								int predefinedValue = perPropertyBrowsing.GetPredefinedValue(this.target.DISPID, dwCookie, variant);
								if (predefinedValue == 0 && variant.vt != 0)
								{
									array[i] = variant.ToObject();
									if (array[i].GetType() != propertyType)
									{
										if (propertyType.IsEnum)
										{
											array[i] = Enum.ToObject(propertyType, array[i]);
										}
										else
										{
											try
											{
												array[i] = Convert.ChangeType(array[i], propertyType, CultureInfo.InvariantCulture);
											}
											catch
											{
											}
										}
									}
								}
								variant.Clear();
								if (predefinedValue == 0)
								{
									num++;
								}
								else if (num > 0)
								{
									Array.Copy(items, i, items, i + 1, num);
									Array.Copy(array, i, array, i + 1, num);
								}
							}
						}
						string[] array2 = new string[num];
						Array.Copy(items, 0, array2, 0, num);
						base.PopulateArrays(array2, array);
					}
				}
				catch (Exception ex)
				{
					base.PopulateArrays(new string[0], new object[0]);
				}
			}

			// Token: 0x0600709C RID: 28828 RVA: 0x000072B6 File Offset: 0x000054B6
			protected override void PopulateArrays(string[] names, object[] values)
			{
			}

			// Token: 0x0600709D RID: 28829 RVA: 0x0019CFC0 File Offset: 0x0019B1C0
			public override object FromString(string s)
			{
				this.EnsureArrays();
				return base.FromString(s);
			}

			// Token: 0x0600709E RID: 28830 RVA: 0x0019CFD0 File Offset: 0x0019B1D0
			public override string ToString(object v)
			{
				if (this.target.IsCurrentValue(v))
				{
					bool flag = false;
					string displayString = Com2IPerPropertyBrowsingHandler.GetDisplayString((NativeMethods.IPerPropertyBrowsing)this.target.TargetObject, this.target.DISPID, ref flag);
					if (flag)
					{
						return displayString;
					}
				}
				this.EnsureArrays();
				return base.ToString(v);
			}

			// Token: 0x0400438F RID: 17295
			internal Com2PropertyDescriptor target;

			// Token: 0x04004390 RID: 17296
			private Com2IPerPropertyBrowsingHandler handler;

			// Token: 0x04004391 RID: 17297
			private OleStrCAMarshaler nameMarshaller;

			// Token: 0x04004392 RID: 17298
			private Int32CAMarshaler valueMarshaller;

			// Token: 0x04004393 RID: 17299
			internal bool arraysFetched;
		}
	}
}
