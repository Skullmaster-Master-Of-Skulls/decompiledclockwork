using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI
{
	// Token: 0x0200027F RID: 639
	public abstract class DataSourceView
	{
		// Token: 0x06001E37 RID: 7735 RVA: 0x000615C0 File Offset: 0x0005F7C0
		protected DataSourceView(IDataSource owner, string viewName)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (viewName == null)
			{
				throw new ArgumentNullException("viewName");
			}
			this._name = viewName;
			DataSourceControl dataSourceControl = owner as DataSourceControl;
			if (dataSourceControl != null)
			{
				dataSourceControl.DataSourceChangedInternal += this.OnDataSourceChangedInternal;
				return;
			}
			owner.DataSourceChanged += this.OnDataSourceChangedInternal;
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool CanDelete
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool CanInsert
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001E3A RID: 7738 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool CanPage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001E3B RID: 7739 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool CanRetrieveTotalRowCount
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool CanSort
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001E3D RID: 7741 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool CanUpdate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x00061625 File Offset: 0x0005F825
		protected EventHandlerList Events
		{
			get
			{
				if (this._events == null)
				{
					this._events = new EventHandlerList();
				}
				return this._events;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001E3F RID: 7743 RVA: 0x00061640 File Offset: 0x0005F840
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06001E40 RID: 7744 RVA: 0x00061648 File Offset: 0x0005F848
		// (remove) Token: 0x06001E41 RID: 7745 RVA: 0x0006165B File Offset: 0x0005F85B
		public event EventHandler DataSourceViewChanged
		{
			add
			{
				this.Events.AddHandler(DataSourceView.EventDataSourceViewChanged, value);
			}
			remove
			{
				this.Events.RemoveHandler(DataSourceView.EventDataSourceViewChanged, value);
			}
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool CanExecute(string commandName)
		{
			return false;
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x00061670 File Offset: 0x0005F870
		public virtual void Delete(IDictionary keys, IDictionary oldValues, DataSourceViewOperationCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			int affectedRecords = 0;
			bool flag = false;
			try
			{
				affectedRecords = this.ExecuteDelete(keys, oldValues);
			}
			catch (Exception ex)
			{
				flag = true;
				if (!callback(affectedRecords, ex))
				{
					throw;
				}
			}
			finally
			{
				if (!flag)
				{
					callback(affectedRecords, null);
				}
			}
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x000616D4 File Offset: 0x0005F8D4
		public virtual void ExecuteCommand(string commandName, IDictionary keys, IDictionary values, DataSourceViewOperationCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			int affectedRecords = 0;
			bool flag = false;
			try
			{
				affectedRecords = this.ExecuteCommand(commandName, keys, values);
			}
			catch (Exception ex)
			{
				flag = true;
				if (!callback(affectedRecords, ex))
				{
					throw;
				}
			}
			finally
			{
				if (!flag)
				{
					callback(affectedRecords, null);
				}
			}
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x00010D64 File Offset: 0x0000EF64
		protected virtual int ExecuteCommand(string commandName, IDictionary keys, IDictionary values)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x00010D64 File Offset: 0x0000EF64
		protected virtual int ExecuteDelete(IDictionary keys, IDictionary oldValues)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x00010D64 File Offset: 0x0000EF64
		protected virtual int ExecuteInsert(IDictionary values)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001E48 RID: 7752
		protected internal abstract IEnumerable ExecuteSelect(DataSourceSelectArguments arguments);

		// Token: 0x06001E49 RID: 7753 RVA: 0x00010D64 File Offset: 0x0000EF64
		protected virtual int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x0006173C File Offset: 0x0005F93C
		private void OnDataSourceChangedInternal(object sender, EventArgs e)
		{
			this.OnDataSourceViewChanged(e);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x00061748 File Offset: 0x0005F948
		protected virtual void OnDataSourceViewChanged(EventArgs e)
		{
			EventHandler eventHandler = this.Events[DataSourceView.EventDataSourceViewChanged] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x00061778 File Offset: 0x0005F978
		public virtual void Insert(IDictionary values, DataSourceViewOperationCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			int affectedRecords = 0;
			bool flag = false;
			try
			{
				affectedRecords = this.ExecuteInsert(values);
			}
			catch (Exception ex)
			{
				flag = true;
				if (!callback(affectedRecords, ex))
				{
					throw;
				}
			}
			finally
			{
				if (!flag)
				{
					callback(affectedRecords, null);
				}
			}
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x000617DC File Offset: 0x0005F9DC
		protected internal virtual void RaiseUnsupportedCapabilityError(DataSourceCapabilities capability)
		{
			if (!this.CanPage && (capability & DataSourceCapabilities.Page) != DataSourceCapabilities.None)
			{
				throw new NotSupportedException(SR.GetString("DataSourceView_NoPaging"));
			}
			if (!this.CanSort && (capability & DataSourceCapabilities.Sort) != DataSourceCapabilities.None)
			{
				throw new NotSupportedException(SR.GetString("DataSourceView_NoSorting"));
			}
			if (!this.CanRetrieveTotalRowCount && (capability & DataSourceCapabilities.RetrieveTotalRowCount) != DataSourceCapabilities.None)
			{
				throw new NotSupportedException(SR.GetString("DataSourceView_NoRowCount"));
			}
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x00061840 File Offset: 0x0005FA40
		public virtual void Select(DataSourceSelectArguments arguments, DataSourceViewSelectCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			callback(this.ExecuteSelect(arguments));
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x00061860 File Offset: 0x0005FA60
		public virtual void Update(IDictionary keys, IDictionary values, IDictionary oldValues, DataSourceViewOperationCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			int affectedRecords = 0;
			bool flag = false;
			try
			{
				affectedRecords = this.ExecuteUpdate(keys, values, oldValues);
			}
			catch (Exception ex)
			{
				flag = true;
				if (!callback(affectedRecords, ex))
				{
					throw;
				}
			}
			finally
			{
				if (!flag)
				{
					callback(affectedRecords, null);
				}
			}
		}

		// Token: 0x0400198B RID: 6539
		private static readonly object EventDataSourceViewChanged = new object();

		// Token: 0x0400198C RID: 6540
		private EventHandlerList _events;

		// Token: 0x0400198D RID: 6541
		private string _name;
	}
}
