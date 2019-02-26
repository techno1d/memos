public static Uri ToUri(this string urlText)
{
  Uri uriResult;
	bool noError = Uri.TryCreate(urlText, UriKind.Absolute, out uriResult)
				&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

	if (noError)
		return uriResult;
	else
		return null;
  }
