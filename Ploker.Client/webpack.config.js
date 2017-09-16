const path = require("path");
const CopyWebpackPlugin = require("copy-webpack-plugin");

module.exports = {
  devtool: "cheap-module-eval-source-map",
  entry: "./src/index.js",
  output: {
    filename: "bundle.js",
    path: path.resolve(__dirname, "../Ploker.Server/wwwroot")
  },
  module: {
    loaders: [
      { test: /\.js$/, exclude: /node_modules/, loader: "babel-loader" }
    ]
  },
  plugins: [new CopyWebpackPlugin([{ from: "src/index.html" }])]
};
