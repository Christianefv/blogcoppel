<template>
	<div id="app">
		<pm-loader></pm-loader>
		<router-view />
	</div>
</template>

<script>
	import PmLoader from "@/components/usuarios/Loader"
	export default {
		components: {
			PmLoader
		}
	}
</script>

<style lang="scss">
</style>


