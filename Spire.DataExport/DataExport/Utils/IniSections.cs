using System;
using System.Collections;
using System.ComponentModel;

namespace Spire.DataExport.Utils
{
	// Token: 0x0200023D RID: 573
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class IniSections : CollectionBase, ICustomTypeDescriptor
	{
		// Token: 0x06001186 RID: 4486 RVA: 0x000BDAB8 File Offset: 0x000BCAB8
		internal IniSections(XMLSetting A_0)
		{
			this.ᜁ = A_0;
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x000BDAD4 File Offset: 0x000BCAD4
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

		// Token: 0x06001188 RID: 4488 RVA: 0x000BDB24 File Offset: 0x000BCB24
		public void Remove(int itemNumber)
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
			base.RemoveAt(itemNumber);
			this.ᜀ = 0;
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x000BDB70 File Offset: 0x000BCB70
		public IniSection Add(string sectionName)
		{
			IniSection iniSection;
			for (;;)
			{
				iniSection = this[sectionName];
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜀ = base.List.Add(new IniSection(sectionName, this.ᜁ));
						iniSection = (IniSection)base.List[this.ᜀ];
						iniSection.ᜃ = this;
						num = 1;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_27;
						default:
							goto IL_8A;
						}
						break;
					case 2:
						if (iniSection == null)
						{
							goto IL_27;
						}
						return iniSection;
					}
					break;
					IL_27:
					num = 0;
				}
			}
			IL_8A:
			if (true)
			{
			}
			if (false)
			{
			}
			return iniSection;
		}

		// Token: 0x1700027F RID: 639
		public IniSection this[int itemNumber]
		{
			get
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
				return (IniSection)base.List[itemNumber];
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

		// Token: 0x17000280 RID: 640
		public IniSection this[string itemName]
		{
			get
			{
				int num;
				for (;;)
				{
					num = this.ᜀ(itemName);
					if (num != -1)
					{
						goto IL_34;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_2C;
					}
				}
				IL_2C:
				if (false)
				{
				}
				return null;
				IL_34:
				return (IniSection)base.List[num];
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
				base.List[this.ᜀ(itemName)] = value;
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x000BDD64 File Offset: 0x000BCD64
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		internal int ᜀ(string A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int count = base.Count;
					int num = 6;
					for (;;)
					{
						IEnumerator enumerator;
						int num2;
						switch (num)
						{
						case 0:
							try
							{
								num = 7;
								int result;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										if (!enumerator.MoveNext())
										{
											num = 6;
											continue;
										}
										IniSection iniSection = (IniSection)enumerator.Current;
										num = 3;
										continue;
									}
									case 1:
										goto IL_10E;
									case 2:
										this.ᜀ = num2;
										result = this.ᜀ;
										num = 1;
										continue;
									case 3:
									{
										IniSection iniSection;
										if (iniSection.Name == A_0)
										{
											num = 2;
											continue;
										}
										num2++;
										num = 5;
										continue;
									}
									case 4:
										goto IL_11F;
									case 6:
										num = 4;
										continue;
									}
									IL_D4:
									num = 0;
									continue;
									goto IL_D4;
								}
								IL_10E:
								return result;
								IL_11F:
								return -1;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable = enumerator as IDisposable;
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_16A;
										case 1:
											disposable.Dispose();
											num = 0;
											continue;
										case 2:
											if (disposable != null)
											{
												num = 1;
												continue;
											}
											goto IL_16C;
										}
										break;
									}
								}
								IL_16A:
								IL_16C:;
							}
							goto IL_16D;
						case 1:
							num = 2;
							continue;
						case 2:
							if (this.ᜀ < count)
							{
								num = 3;
								continue;
							}
							goto IL_16D;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_43;
							default:
								if (false)
								{
								}
								num = 5;
								continue;
							}
							break;
						case 4:
							goto IL_1C3;
						case 5:
							if (((IniSection)base.List[this.ᜀ]).Name == A_0)
							{
								num = 4;
								continue;
							}
							goto IL_16D;
						case 6:
							goto IL_43;
						}
						break;
						IL_43:
						if (count > 0)
						{
							num = 1;
							continue;
						}
						IL_16D:
						num2 = 0;
						enumerator = base.GetEnumerator();
						num = 0;
					}
				}
				IL_1C3:
				if (true)
				{
				}
				return this.ᜀ;
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x000BDF9C File Offset: 0x000BCF9C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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
					{
						sprἭ value = new sprἭ(this[num], attributes);
						propertyDescriptorCollection.Add(value);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BF;
						default:
							if (false)
							{
							}
							num2 = 5;
							continue;
						}
						break;
					}
					case 1:
						if (num >= base.Count)
						{
							num2 = 2;
							continue;
						}
						num2 = 3;
						continue;
					case 2:
						return propertyDescriptorCollection;
					case 3:
						if (this[num].DisplayInPG)
						{
							num2 = 0;
							continue;
						}
						goto IL_3A;
					case 4:
						goto IL_BF;
					case 5:
						goto IL_3A;
					case 6:
						goto IL_BF;
					}
					break;
					IL_3A:
					if (true)
					{
					}
					num++;
					num2 = 4;
					continue;
					IL_BF:
					num2 = 1;
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x000BE08C File Offset: 0x000BD08C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string GetClassName()
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
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x000BE0D0 File Offset: 0x000BD0D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public AttributeCollection GetAttributes()
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
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x000BE114 File Offset: 0x000BD114
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x06001193 RID: 4499 RVA: 0x000BE158 File Offset: 0x000BD158
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public TypeConverter GetConverter()
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
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x000BE19C File Offset: 0x000BD19C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public EventDescriptor GetDefaultEvent()
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
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000BE1E0 File Offset: 0x000BD1E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
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

		// Token: 0x06001196 RID: 4502 RVA: 0x000BE224 File Offset: 0x000BD224
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public object GetEditor(Type editorBaseType)
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
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x000BE268 File Offset: 0x000BD268
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
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
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x000BE2AC File Offset: 0x000BD2AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
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

		// Token: 0x06001199 RID: 4505 RVA: 0x000BE2F0 File Offset: 0x000BD2F0
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

		// Token: 0x0600119A RID: 4506 RVA: 0x000BE32C File Offset: 0x000BD32C
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

		// Token: 0x04000C4F RID: 3151
		private int ᜀ;

		// Token: 0x04000C50 RID: 3152
		private int[] \u25D9\u00A1\u00AC\u0085;

		// Token: 0x04000C51 RID: 3153
		internal XMLSetting ᜁ;
	}
}
