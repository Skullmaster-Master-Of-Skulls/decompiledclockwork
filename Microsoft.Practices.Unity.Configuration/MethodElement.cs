using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Configuration.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x0200002C RID: 44
	public class MethodElement : InjectionMemberElement
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00005795 File Offset: 0x00003995
		public MethodElement()
		{
			this.methodCount = Interlocked.Increment(ref MethodElement.methodElementCounter);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000057AD File Offset: 0x000039AD
		// (set) Token: 0x06000155 RID: 341 RVA: 0x000057BF File Offset: 0x000039BF
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000057CD File Offset: 0x000039CD
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public ParameterElementCollection Parameters
		{
			get
			{
				return (ParameterElementCollection)base[""];
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000057E0 File Offset: 0x000039E0
		public override string Key
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "method:{0}:{1}", new object[]
				{
					this.Name,
					this.methodCount
				});
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000581B File Offset: 0x00003A1B
		public override string ElementName
		{
			get
			{
				return "method";
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005824 File Offset: 0x00003A24
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override void SerializeContent(XmlWriter writer)
		{
			Guard.ArgumentNotNull(writer, "writer");
			writer.WriteAttributeString("name", this.Name);
			foreach (ParameterElement @object in this.Parameters)
			{
				writer.WriteElement("param", new Action<XmlWriter>(@object.SerializeContent));
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000058A0 File Offset: 0x00003AA0
		public override IEnumerable<InjectionMember> GetInjectionMembers(IUnityContainer container, Type fromType, Type toType, string name)
		{
			MethodInfo methodToCall = this.FindMethodInfo(toType);
			this.GuardIsMatchingMethod(toType, methodToCall);
			return new InjectionMember[]
			{
				this.MakeInjectionMember(container, methodToCall)
			};
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000058D0 File Offset: 0x00003AD0
		private MethodInfo FindMethodInfo(Type typeToInitialize)
		{
			return typeToInitialize.GetMethods().Where(new Func<MethodInfo, bool>(this.MethodMatches)).FirstOrDefault<MethodInfo>();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000058F0 File Offset: 0x00003AF0
		private InjectionMember MakeInjectionMember(IUnityContainer container, MethodInfo methodToCall)
		{
			List<InjectionParameterValue> list = new List<InjectionParameterValue>();
			ParameterInfo[] parameters = methodToCall.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				list.Add(this.Parameters[i].GetParameterValue(container, parameters[i].ParameterType));
			}
			return new InjectionMethod(this.Name, list.ToArray());
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000595C File Offset: 0x00003B5C
		private bool MethodMatches(MethodInfo method)
		{
			if (method.Name != this.Name)
			{
				return false;
			}
			ParameterInfo[] parameters = method.GetParameters();
			if (parameters.Length != this.Parameters.Count)
			{
				return false;
			}
			return Sequence.Zip<ParameterElement, ParameterInfo>(this.Parameters, parameters).All((Pair<ParameterElement, ParameterInfo> pair) => pair.First.Matches(pair.Second));
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000059D0 File Offset: 0x00003BD0
		private void GuardIsMatchingMethod(Type typeToInitialize, MethodInfo methodToCall)
		{
			if (methodToCall == null)
			{
				string text = string.Join(", ", (from p in this.Parameters
				select p.Name).ToArray<string>());
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.NoMatchingMethod, new object[]
				{
					typeToInitialize,
					this.Name,
					text
				}));
			}
		}

		// Token: 0x0400004E RID: 78
		private const string NamePropertyName = "name";

		// Token: 0x0400004F RID: 79
		private const string ParametersPropertyName = "";

		// Token: 0x04000050 RID: 80
		private static int methodElementCounter;

		// Token: 0x04000051 RID: 81
		private readonly int methodCount;
	}
}
