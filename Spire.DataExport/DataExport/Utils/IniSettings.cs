using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.Utils
{
	// Token: 0x02000233 RID: 563
	[Editor(typeof(NullCollectionEditor), typeof(UITypeEditor))]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class IniSettings : CollectionBase, ICustomTypeDescriptor
	{
		// Token: 0x06001107 RID: 4359 RVA: 0x000B7840 File Offset: 0x000B6840
		public IniSettings(XMLSetting topObject)
		{
			this.ᜁ = topObject;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x000B785C File Offset: 0x000B685C
		public override string ToString()
		{
			int a_ = 6;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return base.Count + HyperlinksCollectionEditor.b("ȡ眣䌥尧帩䔫䀭圯䄱", a_);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x000B78C0 File Offset: 0x000B68C0
		public void Remove(string itemName)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.RemoveAt(this.ᜀ(itemName));
			this.ᜀ = 0;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x000B7910 File Offset: 0x000B6910
		public void Remove(int itemNumber)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.RemoveAt(itemNumber);
			this.ᜀ = 0;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x000B795C File Offset: 0x000B695C
		public IniSetting Add(string settingName)
		{
			IniSetting iniSetting;
			for (;;)
			{
				IL_30:
				iniSetting = this[settingName];
				int num = 0;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return iniSetting;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							if (iniSetting == null)
							{
								num = 2;
								continue;
							}
							return iniSetting;
						case 1:
							return iniSetting;
						case 2:
							this.ᜀ = base.List.Add(new IniSetting(settingName));
							iniSetting = (IniSetting)base.List[this.ᜀ];
							iniSetting.ᜆ = this;
							iniSetting.ᜉ = this.ᜁ;
							if (true)
							{
							}
							num = 1;
							continue;
						}
						goto IL_30;
					}
				}
			}
			return iniSetting;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x000B7A18 File Offset: 0x000B6A18
		public IniSetting Add(string settingName, object val)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			IniSetting iniSetting = this.Add(settingName);
			iniSetting.Value = val;
			return iniSetting;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x000B7A64 File Offset: 0x000B6A64
		public IniSetting Add(string settingName, object val, string tag)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			IniSetting iniSetting = this.Add(settingName);
			iniSetting.Value = val;
			iniSetting.Tag = tag;
			return iniSetting;
		}

		// Token: 0x17000270 RID: 624
		public IniSetting this[int itemNumber]
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (IniSetting)base.List[itemNumber];
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				base.List[itemNumber] = value;
			}
		}

		// Token: 0x17000271 RID: 625
		public IniSetting this[string itemName]
		{
			get
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = this.ᜀ(itemName);
					if (num == -1)
					{
						return null;
					}
					break;
				}
				return (IniSetting)base.List[num];
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				base.List[this.ᜀ(itemName)] = value;
			}
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x000B7BF8 File Offset: 0x000B6BF8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		internal int ᜀ(string A_0)
		{
			switch (0)
			{
			default:
				if (true)
				{
				}
				for (;;)
				{
					int count = base.Count;
					int num = 4;
					for (;;)
					{
						int num2;
						IEnumerator enumerator;
						switch (num)
						{
						case 0:
							try
							{
								num = 5;
								int result;
								for (;;)
								{
									switch (num)
									{
									case 0:
										this.ᜀ = num2;
										result = this.ᜀ;
										num = 3;
										continue;
									case 2:
										num = 4;
										continue;
									case 3:
										goto IL_116;
									case 4:
										goto IL_127;
									case 6:
									{
										if (!enumerator.MoveNext())
										{
											num = 2;
											continue;
										}
										IniSetting iniSetting = (IniSetting)enumerator.Current;
										num = 7;
										continue;
									}
									case 7:
									{
										IniSetting iniSetting;
										if (iniSetting.Name == A_0)
										{
											num = 0;
											continue;
										}
										num2++;
										num = 1;
										continue;
									}
									}
									IL_DC:
									num = 6;
									continue;
									goto IL_DC;
								}
								IL_116:
								return result;
								IL_127:
								return -1;
							}
							finally
							{
								for (;;)
								{
									IL_15D:
									IDisposable disposable = enumerator as IDisposable;
									num = 0;
									for (;;)
									{
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_190;
										default:
											if (false)
											{
											}
											switch (num)
											{
											case 0:
												if (disposable != null)
												{
													num = 2;
													continue;
												}
												goto IL_190;
											case 1:
												goto IL_18E;
											case 2:
												disposable.Dispose();
												num = 1;
												continue;
											}
											goto IL_15D;
										}
									}
								}
								IL_18E:
								IL_190:;
							}
							goto IL_191;
						case 1:
							if (((IniSetting)base.List[this.ᜀ]).Name == A_0)
							{
								num = 6;
								continue;
							}
							goto IL_191;
						case 2:
							num = 3;
							continue;
						case 3:
							if (this.ᜀ < count)
							{
								num = 5;
								continue;
							}
							goto IL_191;
						case 4:
							if (count > 0)
							{
								num = 2;
								continue;
							}
							goto IL_191;
						case 5:
							num = 1;
							continue;
						case 6:
							goto IL_1E7;
						}
						break;
						IL_191:
						num2 = 0;
						enumerator = base.GetEnumerator();
						num = 0;
					}
				}
				IL_1E7:
				return this.ᜀ;
			}
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x000B7E2C File Offset: 0x000B6E2C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection propertyDescriptorCollection;
			for (;;)
			{
				propertyDescriptorCollection = new PropertyDescriptorCollection(null);
				int num = 0;
				int num2 = 6;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_3A;
					case 1:
						if (this[num].DisplayInPG)
						{
							num2 = 2;
							continue;
						}
						goto IL_3A;
					case 2:
					{
						spr\u20CD value = new spr\u20CD(this[num], attributes);
						propertyDescriptorCollection.Add(value);
						num2 = 0;
						continue;
					}
					case 3:
						goto IL_BF;
					case 4:
						return propertyDescriptorCollection;
					case 5:
						if (num >= base.Count)
						{
							num2 = 4;
							continue;
						}
						num2 = 1;
						continue;
					case 6:
						goto IL_BF;
					}
					break;
					IL_3A:
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num2 = 3;
						continue;
					}
					IL_BF:
					num2 = 5;
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000B7F1C File Offset: 0x000B6F1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public string GetClassName()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000B7F60 File Offset: 0x000B6F60
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public AttributeCollection GetAttributes()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x000B7FA4 File Offset: 0x000B6FA4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public string GetComponentName()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x000B7FE8 File Offset: 0x000B6FE8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public TypeConverter GetConverter()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x000B802C File Offset: 0x000B702C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public EventDescriptor GetDefaultEvent()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x000B8070 File Offset: 0x000B7070
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public PropertyDescriptor GetDefaultProperty()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000B80B4 File Offset: 0x000B70B4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object GetEditor(Type editorBaseType)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x000B80F8 File Offset: 0x000B70F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x000B813C File Offset: 0x000B713C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public EventDescriptorCollection GetEvents()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x000B8180 File Offset: 0x000B7180
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return this;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000B81BC File Offset: 0x000B71BC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public PropertyDescriptorCollection GetProperties()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return TypeDescriptor.GetProperties(this, true);
		}

		// Token: 0x04000C23 RID: 3107
		private int \u25D8\u00A4\u0091\u00B0;

		// Token: 0x04000C24 RID: 3108
		private string[] \u2609\u00AF\u008D\u009A;

		// Token: 0x04000C25 RID: 3109
		private bool \u25D9\u009D\u0085\u0086;

		// Token: 0x04000C26 RID: 3110
		private long[] \u2593\u0094\u009D\u009D;

		// Token: 0x04000C27 RID: 3111
		private int[] \u2460\u0087\u00A7\u00AF;

		// Token: 0x04000C28 RID: 3112
		private int ᜀ;

		// Token: 0x04000C29 RID: 3113
		internal XMLSetting ᜁ;
	}
}
