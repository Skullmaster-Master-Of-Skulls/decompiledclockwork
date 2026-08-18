using System;
using System.Collections.Generic;
using System.Threading;

namespace System.ServiceModel
{
	// Token: 0x0200011E RID: 286
	internal abstract class TypedHeaderManager
	{
		// Token: 0x06000750 RID: 1872 RVA: 0x0001E71B File Offset: 0x0001C91B
		internal static object Create(Type t, object content, bool mustUnderstand, bool relay, string actor)
		{
			return TypedHeaderManager.GetTypedHeaderManager(t).Create(content, mustUnderstand, relay, actor);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001E72D File Offset: 0x0001C92D
		internal static object GetContent(Type t, object typedHeaderInstance, out bool mustUnderstand, out bool relay, out string actor)
		{
			return TypedHeaderManager.GetTypedHeaderManager(t).GetContent(typedHeaderInstance, out mustUnderstand, out relay, out actor);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001E73F File Offset: 0x0001C93F
		internal static Type GetMessageHeaderType(Type contentType)
		{
			return TypedHeaderManager.GetTypedHeaderManager(contentType).GetMessageHeaderType();
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001E74C File Offset: 0x0001C94C
		internal static Type GetHeaderType(Type headerParameterType)
		{
			if (headerParameterType.IsGenericType && headerParameterType.GetGenericTypeDefinition() == typeof(MessageHeader<>))
			{
				return headerParameterType.GetGenericArguments()[0];
			}
			return headerParameterType;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001E778 File Offset: 0x0001C978
		private static TypedHeaderManager GetTypedHeaderManager(Type t)
		{
			TypedHeaderManager typedHeaderManager = null;
			bool flag = false;
			try
			{
				try
				{
				}
				finally
				{
					TypedHeaderManager.cacheLock.AcquireReaderLock(int.MaxValue);
					flag = true;
				}
				if (!TypedHeaderManager.cache.TryGetValue(t, out typedHeaderManager))
				{
					TypedHeaderManager.cacheLock.UpgradeToWriterLock(int.MaxValue);
					if (!TypedHeaderManager.cache.TryGetValue(t, out typedHeaderManager))
					{
						typedHeaderManager = (TypedHeaderManager)Activator.CreateInstance(TypedHeaderManager.GenericAdapterType.MakeGenericType(new Type[]
						{
							t
						}));
						TypedHeaderManager.cache.Add(t, typedHeaderManager);
					}
				}
			}
			finally
			{
				if (flag)
				{
					TypedHeaderManager.cacheLock.ReleaseLock();
				}
			}
			return typedHeaderManager;
		}

		// Token: 0x06000755 RID: 1877
		protected abstract object Create(object content, bool mustUnderstand, bool relay, string actor);

		// Token: 0x06000756 RID: 1878
		protected abstract object GetContent(object typedHeaderInstance, out bool mustUnderstand, out bool relay, out string actor);

		// Token: 0x06000757 RID: 1879
		protected abstract Type GetMessageHeaderType();

		// Token: 0x04000AC2 RID: 2754
		private static Dictionary<Type, TypedHeaderManager> cache = new Dictionary<Type, TypedHeaderManager>();

		// Token: 0x04000AC3 RID: 2755
		private static ReaderWriterLock cacheLock = new ReaderWriterLock();

		// Token: 0x04000AC4 RID: 2756
		private static Type GenericAdapterType = typeof(TypedHeaderManager.GenericAdapter<>);

		// Token: 0x02000AED RID: 2797
		private class GenericAdapter<T> : TypedHeaderManager
		{
			// Token: 0x06006F1A RID: 28442 RVA: 0x0019D218 File Offset: 0x0019B418
			protected override object Create(object content, bool mustUnderstand, bool relay, string actor)
			{
				return new MessageHeader<T>
				{
					Content = (T)((object)content),
					MustUnderstand = mustUnderstand,
					Relay = relay,
					Actor = actor
				};
			}

			// Token: 0x06006F1B RID: 28443 RVA: 0x0019D250 File Offset: 0x0019B450
			protected override object GetContent(object typedHeaderInstance, out bool mustUnderstand, out bool relay, out string actor)
			{
				mustUnderstand = false;
				relay = false;
				actor = null;
				if (typedHeaderInstance == null)
				{
					return null;
				}
				MessageHeader<T> messageHeader = typedHeaderInstance as MessageHeader<T>;
				if (messageHeader == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException("typedHeaderInstance"));
				}
				mustUnderstand = messageHeader.MustUnderstand;
				relay = messageHeader.Relay;
				actor = messageHeader.Actor;
				return messageHeader.Content;
			}

			// Token: 0x06006F1C RID: 28444 RVA: 0x0019D2AF File Offset: 0x0019B4AF
			protected override Type GetMessageHeaderType()
			{
				return typeof(MessageHeader<T>);
			}
		}
	}
}
