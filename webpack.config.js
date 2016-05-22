module.exports = {
  entry: './src/OAuthv2/Content/js/index.js',
  output: { 
    path: __dirname, 
    filename: 'src/OAuthv2/build/bundle.js',
    sourceMapFilename: 'src/OAuthv2/build/bundle.js.map',
  },
  debug: true,
  devtool: 'source-map',
  module: {
    loaders: [
      {
        test: /.jsx?$/,
        loader: 'babel-loader',
        exclude: /node_modules/,
        query: {
          presets: ['es2015', 'react']
        }
      },
      {
        test: /\.css$/,
        loader: 'style!css'
      }
    ]
  },
};