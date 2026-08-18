using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Design;
using System.Globalization;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000170 RID: 368
	public static class DirectiveRegistry
	{
		// Token: 0x06000D1E RID: 3358 RVA: 0x00053557 File Offset: 0x00051757
		static DirectiveRegistry()
		{
			DirectiveRegistry.BuildFrameworkPre40Directives();
			DirectiveRegistry.BuildFramework40Directives();
			DirectiveRegistry.BuildFramework45Directives();
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00053584 File Offset: 0x00051784
		private static void AddCommonDirectives(Version ver)
		{
			DirectiveRegistry.AddDirective(typeof(Assembly), ver, new string[]
			{
				"asax",
				"ashx",
				"asix",
				"asmx",
				"ascx",
				"svc",
				"aspx",
				"master"
			});
			DirectiveRegistry.AddDirective(typeof(Image), ver, new string[]
			{
				"asix"
			});
			DirectiveRegistry.AddDirective(typeof(Implements), ver, new string[]
			{
				"ascx",
				"aspx",
				"master"
			});
			DirectiveRegistry.AddDirective(typeof(Import), ver, new string[]
			{
				"asax",
				"ascx",
				"aspx",
				"master"
			});
			DirectiveRegistry.AddDirective(typeof(MasterType), ver, new string[]
			{
				"aspx",
				"master"
			});
			DirectiveRegistry.AddDirective(typeof(Msgx), ver, new string[]
			{
				"msgx"
			});
			DirectiveRegistry.AddDirective(typeof(OutputCache), ver, new string[]
			{
				"aspx"
			});
			DirectiveRegistry.AddDirective(typeof(OutputCacheAscx), ver, new string[]
			{
				"ascx"
			});
			DirectiveRegistry.AddDirective(typeof(OutputCacheAsix), ver, new string[]
			{
				"asix"
			});
			DirectiveRegistry.AddDirective(typeof(PreviousPageType), ver, new string[]
			{
				"aspx"
			});
			DirectiveRegistry.AddDirective(typeof(Reference), ver, new string[]
			{
				"ascx",
				"aspx",
				"master"
			});
			DirectiveRegistry.AddDirective(typeof(Register), ver, new string[]
			{
				"ascx",
				"aspx",
				"master"
			});
			DirectiveRegistry.AddDirective(typeof(ServiceHost), ver, new string[]
			{
				"svc"
			});
			DirectiveRegistry.AddDirective(typeof(WebService), ver, new string[]
			{
				"asmx"
			});
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000537C0 File Offset: 0x000519C0
		private static void BuildFrameworkPre40Directives()
		{
			Version[] array = new Version[]
			{
				new Version(2, 0),
				new Version(3, 0),
				new Version(3, 5)
			};
			foreach (Version version in array)
			{
				DirectiveRegistry.AddCommonDirectives(version);
				DirectiveRegistry.AddDirective(typeof(Application2_0), version, new string[]
				{
					"asax"
				});
				DirectiveRegistry.AddDirective(typeof(Control2_0), version, new string[]
				{
					"ascx"
				});
				DirectiveRegistry.AddDirective(typeof(Master2_0), version, new string[]
				{
					"master"
				});
				DirectiveRegistry.AddDirective(typeof(Page2_0), version, new string[]
				{
					"aspx"
				});
				DirectiveRegistry.AddDirective(typeof(WebHandler2_0), version, new string[]
				{
					"ashx"
				});
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000538A8 File Offset: 0x00051AA8
		private static void BuildFramework40Directives()
		{
			Version version = new Version(4, 0);
			DirectiveRegistry.AddCommonDirectives(version);
			DirectiveRegistry.AddDirective(typeof(Application4_0), version, new string[]
			{
				"asax"
			});
			DirectiveRegistry.AddDirective(typeof(Control4_0), version, new string[]
			{
				"ascx"
			});
			DirectiveRegistry.AddDirective(typeof(Master4_0), version, new string[]
			{
				"master"
			});
			DirectiveRegistry.AddDirective(typeof(Page4_0), version, new string[]
			{
				"aspx"
			});
			DirectiveRegistry.AddDirective(typeof(WebHandler4_0), version, new string[]
			{
				"ashx"
			});
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0005395C File Offset: 0x00051B5C
		private static void BuildFramework45Directives()
		{
			Version[] array = new Version[]
			{
				new Version(4, 5),
				new Version(4, 6),
				new Version(4, 7),
				new Version(4, 8)
			};
			foreach (Version version in array)
			{
				DirectiveRegistry.AddCommonDirectives(version);
				DirectiveRegistry.AddDirective(typeof(Application4_0), version, new string[]
				{
					"asax"
				});
				DirectiveRegistry.AddDirective(typeof(Control4_5), version, new string[]
				{
					"ascx"
				});
				DirectiveRegistry.AddDirective(typeof(Master4_0), version, new string[]
				{
					"master"
				});
				DirectiveRegistry.AddDirective(typeof(Page4_0), version, new string[]
				{
					"aspx"
				});
				DirectiveRegistry.AddDirective(typeof(WebHandler4_0), version, new string[]
				{
					"ashx"
				});
			}
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00053A50 File Offset: 0x00051C50
		private static void AddDirective(Type directiveType, Version frameworkVersion, string[] extensions)
		{
			Dictionary<string, IList<Type>> dictionary;
			if (!DirectiveRegistry._versionMap.ContainsKey(frameworkVersion))
			{
				dictionary = new Dictionary<string, IList<Type>>();
				DirectiveRegistry._versionMap[frameworkVersion] = dictionary;
			}
			else
			{
				dictionary = DirectiveRegistry._versionMap[frameworkVersion];
			}
			foreach (string key in extensions)
			{
				IList<Type> list;
				if (!dictionary.ContainsKey(key))
				{
					list = new List<Type>();
					dictionary[key] = list;
				}
				else
				{
					list = dictionary[key];
				}
				list.Add(directiveType);
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00053ACC File Offset: 0x00051CCC
		public static ReadOnlyCollection<Type> GetDirectives(Version frameworkVersion, string extension)
		{
			if (!DirectiveRegistry._versionMap.ContainsKey(frameworkVersion))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("DirectiveRegistry_UnknownFramework"), new object[]
				{
					frameworkVersion
				}));
			}
			if (!DirectiveRegistry._versionMap[frameworkVersion].ContainsKey(extension))
			{
				return DirectiveRegistry._emptyList;
			}
			return new ReadOnlyCollection<Type>(DirectiveRegistry._versionMap[frameworkVersion][extension]);
		}

		// Token: 0x040007E8 RID: 2024
		private static Dictionary<Version, Dictionary<string, IList<Type>>> _versionMap = new Dictionary<Version, Dictionary<string, IList<Type>>>();

		// Token: 0x040007E9 RID: 2025
		private static ReadOnlyCollection<Type> _emptyList = new ReadOnlyCollection<Type>(new List<Type>());
	}
}
