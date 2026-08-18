using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020004B7 RID: 1207
	public class TraceSource
	{
		// Token: 0x06002D10 RID: 11536 RVA: 0x000CA551 File Offset: 0x000C8751
		public TraceSource(string name) : this(name, SourceLevels.Off)
		{
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000CA55C File Offset: 0x000C875C
		public TraceSource(string name, SourceLevels defaultLevel)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			this.sourceName = name;
			this.switchLevel = defaultLevel;
			List<WeakReference> obj = TraceSource.tracesources;
			lock (obj)
			{
				TraceSource._pruneCachedTraceSources();
				TraceSource.tracesources.Add(new WeakReference(this));
			}
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000CA5E4 File Offset: 0x000C87E4
		private static void _pruneCachedTraceSources()
		{
			List<WeakReference> obj = TraceSource.tracesources;
			lock (obj)
			{
				if (TraceSource.s_LastCollectionCount != GC.CollectionCount(2))
				{
					List<WeakReference> list = new List<WeakReference>(TraceSource.tracesources.Count);
					for (int i = 0; i < TraceSource.tracesources.Count; i++)
					{
						TraceSource traceSource = (TraceSource)TraceSource.tracesources[i].Target;
						if (traceSource != null)
						{
							list.Add(TraceSource.tracesources[i]);
						}
					}
					if (list.Count < TraceSource.tracesources.Count)
					{
						TraceSource.tracesources.Clear();
						TraceSource.tracesources.AddRange(list);
						TraceSource.tracesources.TrimExcess();
					}
					TraceSource.s_LastCollectionCount = GC.CollectionCount(2);
				}
			}
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x000CA6BC File Offset: 0x000C88BC
		private void Initialize()
		{
			if (!this._initCalled)
			{
				lock (this)
				{
					if (!this._initCalled)
					{
						SourceElementsCollection sources = DiagnosticsConfiguration.Sources;
						if (sources != null)
						{
							SourceElement sourceElement = sources[this.sourceName];
							if (sourceElement != null)
							{
								if (!string.IsNullOrEmpty(sourceElement.SwitchName))
								{
									this.CreateSwitch(sourceElement.SwitchType, sourceElement.SwitchName);
								}
								else
								{
									this.CreateSwitch(sourceElement.SwitchType, this.sourceName);
									if (!string.IsNullOrEmpty(sourceElement.SwitchValue))
									{
										this.internalSwitch.Level = (SourceLevels)Enum.Parse(typeof(SourceLevels), sourceElement.SwitchValue);
									}
								}
								this.listeners = sourceElement.Listeners.GetRuntimeObject();
								this.attributes = new StringDictionary();
								TraceUtils.VerifyAttributes(sourceElement.Attributes, this.GetSupportedAttributes(), this);
								this.attributes.ReplaceHashtable(sourceElement.Attributes);
							}
							else
							{
								this.NoConfigInit();
							}
						}
						else
						{
							this.NoConfigInit();
						}
						this._initCalled = true;
					}
				}
			}
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000CA7F4 File Offset: 0x000C89F4
		private void NoConfigInit()
		{
			this.internalSwitch = new SourceSwitch(this.sourceName, this.switchLevel.ToString());
			this.listeners = new TraceListenerCollection();
			this.listeners.Add(new DefaultTraceListener());
			this.attributes = null;
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000CA850 File Offset: 0x000C8A50
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public void Close()
		{
			if (this.listeners != null)
			{
				object critSec = TraceInternal.critSec;
				lock (critSec)
				{
					foreach (object obj in this.listeners)
					{
						TraceListener traceListener = (TraceListener)obj;
						traceListener.Close();
					}
				}
			}
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000CA8E0 File Offset: 0x000C8AE0
		public void Flush()
		{
			if (this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						using (IEnumerator enumerator = this.listeners.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								TraceListener traceListener = (TraceListener)obj;
								traceListener.Flush();
							}
							return;
						}
					}
				}
				foreach (object obj2 in this.listeners)
				{
					TraceListener traceListener2 = (TraceListener)obj2;
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener obj3 = traceListener2;
						lock (obj3)
						{
							traceListener2.Flush();
							continue;
						}
					}
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000CAA08 File Offset: 0x000C8C08
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000CAA0C File Offset: 0x000C8C0C
		internal static void RefreshAll()
		{
			List<WeakReference> obj = TraceSource.tracesources;
			lock (obj)
			{
				TraceSource._pruneCachedTraceSources();
				for (int i = 0; i < TraceSource.tracesources.Count; i++)
				{
					TraceSource traceSource = (TraceSource)TraceSource.tracesources[i].Target;
					if (traceSource != null)
					{
						traceSource.Refresh();
					}
				}
			}
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x000CAA80 File Offset: 0x000C8C80
		internal void Refresh()
		{
			if (!this._initCalled)
			{
				this.Initialize();
				return;
			}
			SourceElementsCollection sources = DiagnosticsConfiguration.Sources;
			if (sources != null)
			{
				SourceElement sourceElement = sources[this.Name];
				if (sourceElement != null)
				{
					if ((string.IsNullOrEmpty(sourceElement.SwitchType) && this.internalSwitch.GetType() != typeof(SourceSwitch)) || sourceElement.SwitchType != this.internalSwitch.GetType().AssemblyQualifiedName)
					{
						if (!string.IsNullOrEmpty(sourceElement.SwitchName))
						{
							this.CreateSwitch(sourceElement.SwitchType, sourceElement.SwitchName);
						}
						else
						{
							this.CreateSwitch(sourceElement.SwitchType, this.Name);
							if (!string.IsNullOrEmpty(sourceElement.SwitchValue))
							{
								this.internalSwitch.Level = (SourceLevels)Enum.Parse(typeof(SourceLevels), sourceElement.SwitchValue);
							}
						}
					}
					else if (!string.IsNullOrEmpty(sourceElement.SwitchName))
					{
						if (sourceElement.SwitchName != this.internalSwitch.DisplayName)
						{
							this.CreateSwitch(sourceElement.SwitchType, sourceElement.SwitchName);
						}
						else
						{
							this.internalSwitch.Refresh();
						}
					}
					else if (!string.IsNullOrEmpty(sourceElement.SwitchValue))
					{
						this.internalSwitch.Level = (SourceLevels)Enum.Parse(typeof(SourceLevels), sourceElement.SwitchValue);
					}
					else
					{
						this.internalSwitch.Level = SourceLevels.Off;
					}
					TraceListenerCollection traceListenerCollection = new TraceListenerCollection();
					foreach (object obj in sourceElement.Listeners)
					{
						ListenerElement listenerElement = (ListenerElement)obj;
						TraceListener traceListener = this.listeners[listenerElement.Name];
						if (traceListener != null)
						{
							traceListenerCollection.Add(listenerElement.RefreshRuntimeObject(traceListener));
						}
						else
						{
							traceListenerCollection.Add(listenerElement.GetRuntimeObject());
						}
					}
					TraceUtils.VerifyAttributes(sourceElement.Attributes, this.GetSupportedAttributes(), this);
					this.attributes = new StringDictionary();
					this.attributes.ReplaceHashtable(sourceElement.Attributes);
					this.listeners = traceListenerCollection;
					return;
				}
				this.internalSwitch.Level = this.switchLevel;
				this.listeners.Clear();
				this.attributes = null;
			}
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000CACF0 File Offset: 0x000C8EF0
		[Conditional("TRACE")]
		public void TraceEvent(TraceEventType eventType, int id)
		{
			this.Initialize();
			TraceEventCache eventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(eventType) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceEvent(eventCache, this.Name, eventType, id);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener obj = traceListener2;
						lock (obj)
						{
							traceListener2.TraceEvent(eventCache, this.Name, eventType, id);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_111;
						}
						goto IL_F3;
					}
					goto IL_F3;
					IL_111:
					j++;
					continue;
					IL_F3:
					traceListener2.TraceEvent(eventCache, this.Name, eventType, id);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_111;
					}
					goto IL_111;
				}
			}
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000CAE44 File Offset: 0x000C9044
		[Conditional("TRACE")]
		public void TraceEvent(TraceEventType eventType, int id, string message)
		{
			this.Initialize();
			TraceEventCache eventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(eventType) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceEvent(eventCache, this.Name, eventType, id, message);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener obj = traceListener2;
						lock (obj)
						{
							traceListener2.TraceEvent(eventCache, this.Name, eventType, id, message);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_114;
						}
						goto IL_F5;
					}
					goto IL_F5;
					IL_114:
					j++;
					continue;
					IL_F5:
					traceListener2.TraceEvent(eventCache, this.Name, eventType, id, message);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_114;
					}
					goto IL_114;
				}
			}
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000CAF9C File Offset: 0x000C919C
		[Conditional("TRACE")]
		public void TraceEvent(TraceEventType eventType, int id, string format, params object[] args)
		{
			this.Initialize();
			TraceEventCache eventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(eventType) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceEvent(eventCache, this.Name, eventType, id, format, args);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener obj = traceListener2;
						lock (obj)
						{
							traceListener2.TraceEvent(eventCache, this.Name, eventType, id, format, args);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_11D;
						}
						goto IL_FC;
					}
					goto IL_FC;
					IL_11D:
					j++;
					continue;
					IL_FC:
					traceListener2.TraceEvent(eventCache, this.Name, eventType, id, format, args);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_11D;
					}
					goto IL_11D;
				}
			}
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000CB0FC File Offset: 0x000C92FC
		[Conditional("TRACE")]
		public void TraceData(TraceEventType eventType, int id, object data)
		{
			this.Initialize();
			TraceEventCache eventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(eventType) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceData(eventCache, this.Name, eventType, id, data);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener obj = traceListener2;
						lock (obj)
						{
							traceListener2.TraceData(eventCache, this.Name, eventType, id, data);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_114;
						}
						goto IL_F5;
					}
					goto IL_F5;
					IL_114:
					j++;
					continue;
					IL_F5:
					traceListener2.TraceData(eventCache, this.Name, eventType, id, data);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_114;
					}
					goto IL_114;
				}
			}
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000CB254 File Offset: 0x000C9454
		[Conditional("TRACE")]
		public void TraceData(TraceEventType eventType, int id, params object[] data)
		{
			this.Initialize();
			TraceEventCache eventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(eventType) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceData(eventCache, this.Name, eventType, id, data);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener obj = traceListener2;
						lock (obj)
						{
							traceListener2.TraceData(eventCache, this.Name, eventType, id, data);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_114;
						}
						goto IL_F5;
					}
					goto IL_F5;
					IL_114:
					j++;
					continue;
					IL_F5:
					traceListener2.TraceData(eventCache, this.Name, eventType, id, data);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_114;
					}
					goto IL_114;
				}
			}
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x000CB3AC File Offset: 0x000C95AC
		[Conditional("TRACE")]
		public void TraceInformation(string message)
		{
			this.TraceEvent(TraceEventType.Information, 0, message, null);
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000CB3B8 File Offset: 0x000C95B8
		[Conditional("TRACE")]
		public void TraceInformation(string format, params object[] args)
		{
			this.TraceEvent(TraceEventType.Information, 0, format, args);
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000CB3C4 File Offset: 0x000C95C4
		[Conditional("TRACE")]
		public void TraceTransfer(int id, string message, Guid relatedActivityId)
		{
			this.Initialize();
			TraceEventCache eventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(TraceEventType.Transfer) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceTransfer(eventCache, this.Name, id, message, relatedActivityId);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener obj = traceListener2;
						lock (obj)
						{
							traceListener2.TraceTransfer(eventCache, this.Name, id, message, relatedActivityId);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_118;
						}
						goto IL_F9;
					}
					goto IL_F9;
					IL_118:
					j++;
					continue;
					IL_F9:
					traceListener2.TraceTransfer(eventCache, this.Name, id, message, relatedActivityId);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_118;
					}
					goto IL_118;
				}
			}
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000CB520 File Offset: 0x000C9720
		private void CreateSwitch(string typename, string name)
		{
			if (!string.IsNullOrEmpty(typename))
			{
				this.internalSwitch = (SourceSwitch)TraceUtils.GetRuntimeObject(typename, typeof(SourceSwitch), name);
				return;
			}
			this.internalSwitch = new SourceSwitch(name, this.switchLevel.ToString());
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x000CB573 File Offset: 0x000C9773
		public StringDictionary Attributes
		{
			get
			{
				this.Initialize();
				if (this.attributes == null)
				{
					this.attributes = new StringDictionary();
				}
				return this.attributes;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002D24 RID: 11556 RVA: 0x000CB594 File Offset: 0x000C9794
		public string Name
		{
			get
			{
				return this.sourceName;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002D25 RID: 11557 RVA: 0x000CB59E File Offset: 0x000C979E
		public TraceListenerCollection Listeners
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				this.Initialize();
				return this.listeners;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06002D26 RID: 11558 RVA: 0x000CB5AE File Offset: 0x000C97AE
		// (set) Token: 0x06002D27 RID: 11559 RVA: 0x000CB5BE File Offset: 0x000C97BE
		public SourceSwitch Switch
		{
			get
			{
				this.Initialize();
				return this.internalSwitch;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Switch");
				}
				this.Initialize();
				this.internalSwitch = value;
			}
		}

		// Token: 0x04002707 RID: 9991
		private static List<WeakReference> tracesources = new List<WeakReference>();

		// Token: 0x04002708 RID: 9992
		private static int s_LastCollectionCount;

		// Token: 0x04002709 RID: 9993
		private volatile SourceSwitch internalSwitch;

		// Token: 0x0400270A RID: 9994
		private volatile TraceListenerCollection listeners;

		// Token: 0x0400270B RID: 9995
		private StringDictionary attributes;

		// Token: 0x0400270C RID: 9996
		private SourceLevels switchLevel;

		// Token: 0x0400270D RID: 9997
		private volatile string sourceName;

		// Token: 0x0400270E RID: 9998
		internal volatile bool _initCalled;
	}
}
