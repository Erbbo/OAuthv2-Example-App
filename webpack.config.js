module.exports = {
  entry: './src/OAuth-React.Net/Content/js/index.js',
  output: { 
    path: __dirname, 
    filename: 'src/OAuth-React.Net/build/bundle.js',
    sourceMapFilename: 'src/OAuth-React.Net/build/bundle.js.map',
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