using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;

namespace System.Web.Mvc
{
	// Token: 0x020001A7 RID: 423
	internal static class DescriptorUtil
	{
		// Token: 0x06000BC6 RID: 3014 RVA: 0x0001EC74 File Offset: 0x0001CE74
		private static void AppendPartToUniqueIdBuilder(StringBuilder builder, object part)
		{
			if (part == null)
			{
				builder.Append("[-1]");
				return;
			}
			string text = Convert.ToString(part, CultureInfo.InvariantCulture);
			builder.AppendFormat("[{0}]{1}", text.Length, text);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0001ECB8 File Offset: 0x0001CEB8
		public static void AppendUniqueId(StringBuilder builder, object part)
		{
			MemberInfo memberInfo = part as MemberInfo;
			if (memberInfo != null)
			{
				DescriptorUtil.AppendPartToUniqueIdBuilder(builder, memberInfo.Module.ModuleVersionId);
				DescriptorUtil.AppendPartToUniqueIdBuilder(builder, memberInfo.MetadataToken);
				return;
			}
			IUniquelyIdentifiable uniquelyIdentifiable = part as IUniquelyIdentifiable;
			if (uniquelyIdentifiable != null)
			{
				DescriptorUtil.AppendPartToUniqueIdBuilder(builder, uniquelyIdentifiable.UniqueId);
				return;
			}
			DescriptorUtil.AppendPartToUniqueIdBuilder(builder, part);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0001ED1C File Offset: 0x0001CF1C
		public static string CreateUniqueId(object part0, object part1)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DescriptorUtil.AppendUniqueId(stringBuilder, part0);
			DescriptorUtil.AppendUniqueId(stringBuilder, part1);
			return stringBuilder.ToString();
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0001ED44 File Offset: 0x0001CF44
		public static string CreateUniqueId(object part0, object part1, object part2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DescriptorUtil.AppendUniqueId(stringBuilder, part0);
			DescriptorUtil.AppendUniqueId(stringBuilder, part1);
			DescriptorUtil.AppendUniqueId(stringBuilder, part2);
			return stringBuilder.ToString();
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0001ED74 File Offset: 0x0001CF74
		public static TDescriptor[] LazilyFetchOrCreateDescriptors<TReflection, TDescriptor, TArgument>(ref TDescriptor[] cacheLocation, Func<TArgument, TReflection[]> initializer, Func<TReflection, TArgument, TDescriptor> converter, TArgument state)
		{
			TDescriptor[] array = Interlocked.CompareExchange<TDescriptor[]>(ref cacheLocation, null, null);
			if (array != null)
			{
				return array;
			}
			TReflection[] array2 = initializer(state);
			List<TDescriptor> list = new List<TDescriptor>(array2.Length);
			for (int i = 0; i < array2.Length; i++)
			{
				TDescriptor tdescriptor = converter(array2[i], state);
				if (tdescriptor != null)
				{
					list.Add(tdescriptor);
				}
			}
			TDescriptor[] array3 = list.ToArray();
			TDescriptor[] array4 = Interlocked.CompareExchange<TDescriptor[]>(ref cacheLocation, array3, null);
			return array4 ?? array3;
		}
	}
}
