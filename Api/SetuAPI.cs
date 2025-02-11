﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using ArchiSteamFarm;
using Newtonsoft.Json;
using ArchiSteamFarm.Web;
using ArchiSteamFarm.Web.Responses;

namespace ArchiSteamFarm.CustomPlugins.Rin.Api
{
	internal static class SetuAPI
	{
		private const string URL = "https://el-bot-api.vercel.app/api";
		private const string URL_debug = "https://api.lolicon.app/setu/v2";

		internal static async Task<string?> GetRandomSetuURL(WebBrowser webBrowser)
		{
			if (webBrowser == null)
			{
				throw new ArgumentNullException(nameof(webBrowser));
			}

			Uri request = new($"{URL}/setu");

			ObjectResponse<SetuResponse>? response = await webBrowser.UrlGetToJsonObject<SetuResponse>(request).ConfigureAwait(false);

			if (response == null)
			{
				return null;
			}

			if (string.IsNullOrEmpty(response.Content.Link))
			{
				throw new InvalidOperationException(nameof(response.Content.Link));
			}

			return response.Content.Link;
		}
		
		internal static async Task<string?> GetRandomSetuR18URL(WebBrowser webBrowser)
		{
			if (webBrowser == null)
			{
				throw new ArgumentNullException(nameof(webBrowser));
			}

			Uri request = new($"{URL_debug}/?size=regular&r18=1");

			ObjectResponse<LoliconJson>? response = await webBrowser.UrlGetToJsonObject<LoliconJson>(request).ConfigureAwait(false);

			if (response == null)
			{
				return null;
			}

			if (string.IsNullOrEmpty(response.Content.data[0].urls.regular))
			{
				throw new InvalidOperationException(nameof(response.Content.data));
			}

			return response.Content.data[0].urls.regular;
		}
		
		private class LoliconJson
		{
			public string error { get; set; }
			public List<SetuImageJson> data { get; set; }
		}

		private class SetuImageJson
		{
			public int pid { get; set; }
			public int p { get; set; }
			public int uid { get; set; }
			public string title { get; set; }
			public string author { get; set; }
			public Urls urls { get; set; }
			public bool r18 { get; set; }
			public int width { get; set; }
			public int height { get; set; }
			public List<string> tags { get; set; }
			public string ext { get; set; }
			public long uploadDate { get; set; }
		}
		
		private class Urls
		{
			public string regular { get; set; }
			//public string original { get; set; }
		}

		[SuppressMessage("ReSharper", "ClassCannotBeInstantiated")]
		private sealed class SetuResponse
		{
			[JsonProperty(PropertyName = "pid", Required = Required.Always)]
			internal readonly string Pid = "";
			
			[JsonProperty(PropertyName = "p", Required = Required.Always)]
			internal readonly int P;
			
			[JsonProperty(PropertyName = "uid", Required = Required.Always)]
			internal readonly int Uid;
			
			[JsonProperty(PropertyName = "title", Required = Required.Always)]
			internal readonly string Title = "";
			
			[JsonProperty(PropertyName = "author", Required = Required.Always)]
			internal readonly string Author = "";
			
			[JsonProperty(PropertyName = "url", Required = Required.Always)]
			internal readonly string Link = "";
			
			[JsonProperty(PropertyName = "r18", Required = Required.Always)]
			internal readonly bool R18;
			
			[JsonProperty(PropertyName = "width", Required = Required.Always)]
			internal readonly int Width = 0;
			
			[JsonProperty(PropertyName = "height", Required = Required.Always)]
			internal readonly int Height = 0;
			
			[JsonProperty(PropertyName = "tags", Required = Required.Always)]
			internal readonly string[] Tags = null;

			[JsonConstructor]
			private SetuResponse() { }
		}
		
	}
}
