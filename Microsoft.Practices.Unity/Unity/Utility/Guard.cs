using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Microsoft.Practices.Unity.Properties;

namespace Microsoft.Practices.Unity.Utility
{
	// Token: 0x020000A0 RID: 160
	public static class Guard
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x00009446 File Offset: 0x00007646
		public static void ArgumentNotNull(object argumentValue, string argumentName)
		{
			if (argumentValue == null)
			{
				throw new ArgumentNullException(argumentName);
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00009452 File Offset: 0x00007652
		public static void ArgumentNotNullOrEmpty(string argumentValue, string argumentName)
		{
			if (argumentValue == null)
			{
				throw new ArgumentNullException(argumentName);
			}
			if (argumentValue.Length == 0)
			{
				throw new ArgumentException(Resources.ArgumentMustNotBeEmpty, argumentName);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00009474 File Offset: 0x00007674
		public static void TypeIsAssignable(Type assignmentTargetType, Type assignmentValueType, string argumentName)
		{
			if (assignmentTargetType == null)
			{
				throw new ArgumentNullException("assignmentTargetType");
			}
			if (assignmentValueType == null)
			{
				throw new ArgumentNullException("assignmentValueType");
			}
			if (!assignmentTargetType.GetTypeInfo().IsAssignableFrom(assignmentValueType.GetTypeInfo()))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.TypesAreNotAssignable, new object[]
				{
					assignmentTargetType,
					assignmentValueType
				}), argumentName);
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000094D8 File Offset: 0x000076D8
		public static void InstanceIsAssignable(Type assignmentTargetType, object assignmentInstance, string argumentName)
		{
			if (assignmentTargetType == null)
			{
				throw new ArgumentNullException("assignmentTargetType");
			}
			if (assignmentInstance == null)
			{
				throw new ArgumentNullException("assignmentInstance");
			}
			if (!assignmentTargetType.GetTypeInfo().IsAssignableFrom(assignmentInstance.GetType().GetTypeInfo()))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.TypesAreNotAssignable, new object[]
				{
					assignmentTargetType,
					Guard.GetTypeName(assignmentInstance)
				}), argumentName);
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00009544 File Offset: 0x00007744
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Need to use exception as flow control here, no other choice")]
		private static string GetTypeName(object assignmentInstance)
		{
			string result;
			try
			{
				result = assignmentInstance.GetType().FullName;
			}
			catch (Exception)
			{
				result = Resources.UnknownType;
			}
			return result;
		}
	}
}
