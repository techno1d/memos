private async Task<HttpResponseMessage> SendAsyncRequest(HttpRequestMessage request)
		{
			return await Task.Run(() => {
				var cancelSource = new CancellationTokenSource();
				var reqTask = HttpClient.SendAsync(request, cancelSource.Token);

				if (Task.WaitAny(new Task[] { reqTask }, REQUEST_TIMEOUT) < 0)
				{
					cancelSource.Cancel(); // attempt to cancel the HTTP request
					throw new HttpRequestException(TIMEOUT_ERROR_MESSSAGE);
				}

				return reqTask.GetAwaiter().GetResult();
			}).ConfigureAwait(false);
		}
