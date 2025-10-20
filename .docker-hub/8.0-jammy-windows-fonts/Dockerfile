FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy

# Install timezone data, Excel libraries, and font support in a single layer
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        tzdata \
        libc6-dev \
        libgdiplus \
        libx11-dev \
        fontconfig \
    && rm -rf /var/lib/apt/lists/* \
    && apt-get clean

# Copy fonts to container (assuming fonts folder is in build context)
COPY fonts/ /usr/share/fonts/truetype/custom/

# Update font cache
RUN fc-cache -f -v

# Set working directory
WORKDIR /app

# Create non-root user for security
RUN groupadd -r appuser && useradd -r -g appuser appuser
USER appuser

# Expose port (adjust as needed for your app)
EXPOSE 8080