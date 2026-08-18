using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004B1 RID: 1201
	internal class QueryResult<TResult> : IEnumerable<KeyValuePair<MessageQuery, TResult>>, IEnumerable
	{
		// Token: 0x06002DDC RID: 11740 RVA: 0x000B2BE0 File Offset: 0x000B0DE0
		internal QueryResult(QueryMatcher matcher, Message message, bool evalBody)
		{
			this.matcher = matcher;
			this.message = message;
			this.evalBody = evalBody;
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000B2C00 File Offset: 0x000B0E00
		public TResult GetSingleResult()
		{
			QueryProcessor queryProcessor = this.matcher.CreateProcessor();
			XPathResult queryResult;
			try
			{
				queryProcessor.Eval(this.matcher.RootOpcode, this.message, this.evalBody);
			}
			catch (XPathNavigatorException ex)
			{
				throw TraceUtility.ThrowHelperError(ex.Process(this.matcher.RootOpcode), this.message);
			}
			catch (NavigatorInvalidBodyAccessException ex2)
			{
				throw TraceUtility.ThrowHelperError(ex2.Process(this.matcher.RootOpcode), this.message);
			}
			finally
			{
				if (this.evalBody)
				{
					this.message.Close();
				}
				queryResult = queryProcessor.QueryResult;
				this.matcher.ReleaseProcessor(queryProcessor);
			}
			if (typeof(TResult) == typeof(XPathResult) || typeof(TResult) == typeof(object))
			{
				return (TResult)((object)queryResult);
			}
			if (typeof(TResult) == typeof(string))
			{
				return (TResult)((object)queryResult.GetResultAsString());
			}
			if (typeof(TResult) == typeof(bool))
			{
				return (TResult)((object)queryResult.GetResultAsBoolean());
			}
			throw Fx.AssertAndThrowFatal("unsupported type");
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000B2D60 File Offset: 0x000B0F60
		public IEnumerator<KeyValuePair<MessageQuery, TResult>> GetEnumerator()
		{
			QueryProcessor queryProcessor = this.matcher.CreateProcessor();
			Collection<KeyValuePair<MessageQuery, XPathResult>> collection = new Collection<KeyValuePair<MessageQuery, XPathResult>>();
			queryProcessor.ResultSet = collection;
			IEnumerator<KeyValuePair<MessageQuery, TResult>> result;
			try
			{
				queryProcessor.Eval(this.matcher.RootOpcode, this.message, this.evalBody);
				if (typeof(TResult) == typeof(XPathResult))
				{
					result = (IEnumerator<KeyValuePair<MessageQuery, TResult>>)collection.GetEnumerator();
				}
				else
				{
					if (!(typeof(TResult) == typeof(string)) && !(typeof(TResult) == typeof(bool)) && !(typeof(TResult) == typeof(object)))
					{
						throw Fx.AssertAndThrowFatal("unsupported type");
					}
					Collection<KeyValuePair<MessageQuery, TResult>> collection2 = new Collection<KeyValuePair<MessageQuery, TResult>>();
					foreach (KeyValuePair<MessageQuery, XPathResult> keyValuePair in collection)
					{
						if (typeof(TResult) == typeof(string))
						{
							collection2.Add(new KeyValuePair<MessageQuery, TResult>(keyValuePair.Key, (TResult)((object)keyValuePair.Value.GetResultAsString())));
						}
						else if (typeof(TResult) == typeof(bool))
						{
							collection2.Add(new KeyValuePair<MessageQuery, TResult>(keyValuePair.Key, (TResult)((object)keyValuePair.Value.GetResultAsBoolean())));
						}
						else
						{
							collection2.Add(new KeyValuePair<MessageQuery, TResult>(keyValuePair.Key, (TResult)((object)keyValuePair.Value)));
						}
					}
					result = collection2.GetEnumerator();
				}
			}
			catch (XPathNavigatorException ex)
			{
				throw TraceUtility.ThrowHelperError(ex.Process(this.matcher.RootOpcode), this.message);
			}
			catch (NavigatorInvalidBodyAccessException ex2)
			{
				throw TraceUtility.ThrowHelperError(ex2.Process(this.matcher.RootOpcode), this.message);
			}
			finally
			{
				if (this.evalBody)
				{
					this.message.Close();
				}
				this.matcher.ReleaseProcessor(queryProcessor);
			}
			return result;
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x000B2FD8 File Offset: 0x000B11D8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040024EC RID: 9452
		private bool evalBody;

		// Token: 0x040024ED RID: 9453
		private QueryMatcher matcher;

		// Token: 0x040024EE RID: 9454
		private Message message;
	}
}
