import nodeResolve from "@rollup/plugin-node-resolve";

export default {
	input: "game.js",
	output: {
		file: "Web_Root/index.js",
		format: "iife",
	},
	plugins: [
		nodeResolve({
			"extensions": [".js"],
			"moduleDirectories": ["node_modules", "Front_End"],
			"modulesOnly": true
		})
	],
}

