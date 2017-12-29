using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using FeedLibrary.FeedParsers;
using FeedLibrary.FeedTypeParsers;

namespace FeedLibrary
{
    public static class DependencyInjection
    {
        public static void AddFeedLibraryDependencies(this IServiceCollection services)
        {
            // Register Atom parser
            services.AddSingleton<IFeedTypeParser, AtomTypeParser>();
            services.AddSingleton<IFeedParser, AtomParser>();
            
            // Register RSS 2.0 parser
            services.AddSingleton<IFeedTypeParser, RssTypeParser>();
            services.AddSingleton<IFeedParser, RssParser>();

            // Register FeedExtractor
            services.AddSingleton<FeedExtractor>();
        }
    }
}
