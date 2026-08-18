using System;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x020005C1 RID: 1473
	[AttributeUsage(AttributeTargets.All)]
	public class PropertyTabAttribute : Attribute
	{
		// Token: 0x06003732 RID: 14130 RVA: 0x000EFF83 File Offset: 0x000EE183
		public PropertyTabAttribute()
		{
			this.tabScopes = new PropertyTabScope[0];
			this.tabClassNames = new string[0];
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000EFFA3 File Offset: 0x000EE1A3
		public PropertyTabAttribute(Type tabClass) : this(tabClass, PropertyTabScope.Component)
		{
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000EFFAD File Offset: 0x000EE1AD
		public PropertyTabAttribute(string tabClassName) : this(tabClassName, PropertyTabScope.Component)
		{
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000EFFB8 File Offset: 0x000EE1B8
		public PropertyTabAttribute(Type tabClass, PropertyTabScope tabScope)
		{
			this.tabClasses = new Type[]
			{
				tabClass
			};
			if (tabScope < PropertyTabScope.Document)
			{
				throw new ArgumentException(SR.GetString("PropertyTabAttributeBadPropertyTabScope"), "tabScope");
			}
			this.tabScopes = new PropertyTabScope[]
			{
				tabScope
			};
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000F0004 File Offset: 0x000EE204
		public PropertyTabAttribute(string tabClassName, PropertyTabScope tabScope)
		{
			this.tabClassNames = new string[]
			{
				tabClassName
			};
			if (tabScope < PropertyTabScope.Document)
			{
				throw new ArgumentException(SR.GetString("PropertyTabAttributeBadPropertyTabScope"), "tabScope");
			}
			this.tabScopes = new PropertyTabScope[]
			{
				tabScope
			};
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06003737 RID: 14135 RVA: 0x000F0050 File Offset: 0x000EE250
		public Type[] TabClasses
		{
			get
			{
				if (this.tabClasses == null && this.tabClassNames != null)
				{
					this.tabClasses = new Type[this.tabClassNames.Length];
					for (int i = 0; i < this.tabClassNames.Length; i++)
					{
						int num = this.tabClassNames[i].IndexOf(',');
						string text = null;
						string text2;
						if (num != -1)
						{
							text2 = this.tabClassNames[i].Substring(0, num).Trim();
							text = this.tabClassNames[i].Substring(num + 1).Trim();
						}
						else
						{
							text2 = this.tabClassNames[i];
						}
						this.tabClasses[i] = Type.GetType(text2, false);
						if (this.tabClasses[i] == null)
						{
							if (text == null)
							{
								throw new TypeLoadException(SR.GetString("PropertyTabAttributeTypeLoadException", new object[]
								{
									text2
								}));
							}
							Assembly assembly = Assembly.Load(text);
							if (assembly != null)
							{
								this.tabClasses[i] = assembly.GetType(text2, true);
							}
						}
					}
				}
				return this.tabClasses;
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06003738 RID: 14136 RVA: 0x000F0154 File Offset: 0x000EE354
		protected string[] TabClassNames
		{
			get
			{
				if (this.tabClassNames != null)
				{
					return (string[])this.tabClassNames.Clone();
				}
				return null;
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06003739 RID: 14137 RVA: 0x000F0170 File Offset: 0x000EE370
		public PropertyTabScope[] TabScopes
		{
			get
			{
				return this.tabScopes;
			}
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000F0178 File Offset: 0x000EE378
		public override bool Equals(object other)
		{
			return other is PropertyTabAttribute && this.Equals((PropertyTabAttribute)other);
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x000F0190 File Offset: 0x000EE390
		public bool Equals(PropertyTabAttribute other)
		{
			if (other == this)
			{
				return true;
			}
			if (other.TabClasses.Length != this.TabClasses.Length || other.TabScopes.Length != this.TabScopes.Length)
			{
				return false;
			}
			for (int i = 0; i < this.TabClasses.Length; i++)
			{
				if (this.TabClasses[i] != other.TabClasses[i] || this.TabScopes[i] != other.TabScopes[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x000F0208 File Offset: 0x000EE408
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000F0210 File Offset: 0x000EE410
		protected void InitializeArrays(string[] tabClassNames, PropertyTabScope[] tabScopes)
		{
			this.InitializeArrays(tabClassNames, null, tabScopes);
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000F021B File Offset: 0x000EE41B
		protected void InitializeArrays(Type[] tabClasses, PropertyTabScope[] tabScopes)
		{
			this.InitializeArrays(null, tabClasses, tabScopes);
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000F0228 File Offset: 0x000EE428
		private void InitializeArrays(string[] tabClassNames, Type[] tabClasses, PropertyTabScope[] tabScopes)
		{
			if (tabClasses != null)
			{
				if (tabScopes != null && tabClasses.Length != tabScopes.Length)
				{
					throw new ArgumentException(SR.GetString("PropertyTabAttributeArrayLengthMismatch"));
				}
				this.tabClasses = (Type[])tabClasses.Clone();
			}
			else if (tabClassNames != null)
			{
				if (tabScopes != null && tabClasses.Length != tabScopes.Length)
				{
					throw new ArgumentException(SR.GetString("PropertyTabAttributeArrayLengthMismatch"));
				}
				this.tabClassNames = (string[])tabClassNames.Clone();
				this.tabClasses = null;
			}
			else if (this.tabClasses == null && this.tabClassNames == null)
			{
				throw new ArgumentException(SR.GetString("PropertyTabAttributeParamsBothNull"));
			}
			if (tabScopes != null)
			{
				for (int i = 0; i < tabScopes.Length; i++)
				{
					if (tabScopes[i] < PropertyTabScope.Document)
					{
						throw new ArgumentException(SR.GetString("PropertyTabAttributeBadPropertyTabScope"));
					}
				}
				this.tabScopes = (PropertyTabScope[])tabScopes.Clone();
				return;
			}
			this.tabScopes = new PropertyTabScope[tabClasses.Length];
			for (int j = 0; j < this.TabScopes.Length; j++)
			{
				this.tabScopes[j] = PropertyTabScope.Component;
			}
		}

		// Token: 0x04002AD6 RID: 10966
		private PropertyTabScope[] tabScopes;

		// Token: 0x04002AD7 RID: 10967
		private Type[] tabClasses;

		// Token: 0x04002AD8 RID: 10968
		private string[] tabClassNames;
	}
}
