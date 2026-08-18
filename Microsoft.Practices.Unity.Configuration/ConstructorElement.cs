using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Configuration.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x0200001C RID: 28
	public class ConstructorElement : InjectionMemberElement
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004932 File Offset: 0x00002B32
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public ParameterElementCollection Parameters
		{
			get
			{
				return (ParameterElementCollection)base[""];
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004944 File Offset: 0x00002B44
		public override string Key
		{
			get
			{
				return "constructor";
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000494B File Offset: 0x00002B4B
		public override string ElementName
		{
			get
			{
				return "constructor";
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004954 File Offset: 0x00002B54
		public override void SerializeContent(XmlWriter writer)
		{
			foreach (ParameterElement @object in this.Parameters)
			{
				writer.WriteElement("param", new Action<XmlWriter>(@object.SerializeContent));
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000049B4 File Offset: 0x00002BB4
		public override IEnumerable<InjectionMember> GetInjectionMembers(IUnityContainer container, Type fromType, Type toType, string name)
		{
			ConstructorInfo constructorInfo = this.FindConstructorInfo(toType);
			this.GuardIsMatchingConstructor(toType, constructorInfo);
			return new InjectionMember[]
			{
				this.MakeInjectionMember(container, constructorInfo)
			};
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000049E6 File Offset: 0x00002BE6
		private ConstructorInfo FindConstructorInfo(Type typeToConstruct)
		{
			return typeToConstruct.GetConstructors().Where(new Func<ConstructorInfo, bool>(this.ConstructorMatches)).FirstOrDefault<ConstructorInfo>();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004A18 File Offset: 0x00002C18
		private bool ConstructorMatches(ConstructorInfo candiateConstructor)
		{
			ParameterInfo[] parameters = candiateConstructor.GetParameters();
			if (parameters.Length != this.Parameters.Count)
			{
				return false;
			}
			return Sequence.Zip<ParameterElement, ParameterInfo>(this.Parameters, parameters).All((Pair<ParameterElement, ParameterInfo> pair) => pair.First.Matches(pair.Second));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004A6C File Offset: 0x00002C6C
		private InjectionMember MakeInjectionMember(IUnityContainer container, ConstructorInfo constructorToCall)
		{
			List<InjectionParameterValue> list = new List<InjectionParameterValue>();
			ParameterInfo[] parameters = constructorToCall.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				list.Add(this.Parameters[i].GetParameterValue(container, parameters[i].ParameterType));
			}
			return new InjectionConstructor(list.ToArray());
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004AC8 File Offset: 0x00002CC8
		private void GuardIsMatchingConstructor(Type typeToConstruct, ConstructorInfo ctor)
		{
			if (ctor == null)
			{
				string text = string.Join(", ", (from p in this.Parameters
				select p.Name).ToArray<string>());
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.NoMatchingConstructor, new object[]
				{
					typeToConstruct.FullName,
					text
				}));
			}
		}

		// Token: 0x04000030 RID: 48
		private const string ParametersPropertyName = "";
	}
}
