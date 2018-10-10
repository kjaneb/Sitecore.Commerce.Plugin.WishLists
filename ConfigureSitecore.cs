// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigureSitecore.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions;
using Sitecore.Framework.Pipelines.Definitions.Extensions;

namespace Sitecore.Commerce.Plugin.WishLists
{
    /// <summary>
    /// The configure sitecore class.
    /// </summary>
    public class ConfigureSitecore : IConfigureSitecore
    {
        /// <summary>
        /// The configure services.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);

            services.Sitecore().Pipelines(config => config
                .AddPipeline<IGetWishListPipeline, GetWishListPipeline>(definition => definition.Add<GetWishListBlock>())
                .AddPipeline<IAddWishListLinePipeline, AddWishListLinePipeline>(definition => definition.Add<AddWishListLineBlock>())
                .AddPipeline<IRemoveWishListLinePipeline, RemoveWishListLinePipeline>(definition => definition.Add<RemoveWishListLineBlock>())
                .AddPipeline<IConvertWishListToCartPipeline, ConvertWishListToCartPipeline>(definition => definition.Add<ConvertWishListToCartBlock>())
                .ConfigurePipeline<IConfigureServiceApiPipeline>(configure => configure.Add<ConfigureServiceApiBlock>())
                .ConfigurePipeline<IRunningPluginsPipeline>(configure => configure.Add<RegisteredPluginBlock>().After<RunningPluginsBlock>())
            );

            services.RegisterAllCommands(assembly);
        }
    }
}