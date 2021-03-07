// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Microsoft.Extensions.Options;
using Squidex.Hosting.Web;
using Xunit;

namespace Squidex.Hosting.Tests
{
    public class UrlGeneratorTests
    {
        [Theory]
        [InlineData("http://squidex.io")]
        [InlineData("http://squidex.io/")]
        public void Should_build_url_from_options(string url)
        {
            var sut = new UrlGenerator(Options.Create(new UrlOptions
            {
                BaseUrl = url,
                BasePath = null
            }));

            Assert.Equal("http://squidex.io", sut.BuildUrl());

            Assert.Equal("http://squidex.io", sut.BuildUrl("/", false));
            Assert.Equal("http://squidex.io", sut.BuildCallbackUrl("/", false));

            Assert.Equal("http://squidex.io/", sut.BuildUrl("/", true));
            Assert.Equal("http://squidex.io/", sut.BuildCallbackUrl("/", true));

            Assert.Equal("http://squidex.io/path", sut.BuildUrl("/path", false));
            Assert.Equal("http://squidex.io/path", sut.BuildUrl("/path/", false));

            Assert.Equal("http://squidex.io/path/", sut.BuildUrl("/path", true));
            Assert.Equal("http://squidex.io/path/", sut.BuildUrl("/path/", true));
        }

        [Theory]
        [InlineData("http://squidex.io")]
        [InlineData("http://squidex.io/")]
        [InlineData("http://squidex.io/base")]
        [InlineData("http://squidex.io/base/")]
        public void Should_build_url_from_options_with_base_path(string url)
        {
            var sut = new UrlGenerator(Options.Create(new UrlOptions
            {
                BaseUrl = url,
                BasePath = "base"
            }));

            Assert.Equal("http://squidex.io/base", sut.BuildUrl());

            Assert.Equal("http://squidex.io/base", sut.BuildUrl("/", false));
            Assert.Equal("http://squidex.io/base", sut.BuildCallbackUrl("/", false));

            Assert.Equal("http://squidex.io/base/", sut.BuildUrl("/", true));
            Assert.Equal("http://squidex.io/base/", sut.BuildCallbackUrl("/", true));

            Assert.Equal("http://squidex.io/base/path", sut.BuildUrl("/path", false));
            Assert.Equal("http://squidex.io/base/path", sut.BuildUrl("/path/", false));

            Assert.Equal("http://squidex.io/base/path/", sut.BuildUrl("/path", true));
            Assert.Equal("http://squidex.io/base/path/", sut.BuildUrl("/path/", true));
        }
    }
}
