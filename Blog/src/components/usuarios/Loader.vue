<template>
	<div id="loading-spinner" v-show="loading">
		<div>
			<div class="m-2">
				<i class="fas fa-spinner fa-3x color-white-1 fa-spin"></i>
			</div>
			<span class="font-14 color-white-1">{{msg}}</span>
		</div>
	</div>
</template>

<script>
	export default {
		computed: {
			loading() {
				return this.$store.state.loading
			},
			msg() {
				return this.$store.state.msgLoading
			}
		}
	}
</script>

<style>
	#loading-spinner {
		position: fixed;
		width: 100%;
		height: 100%;
		background: rgba(51, 81, 152, 0.7);
		z-index: 1000;
		text-align: center;
		vertical-align: middle;
		padding: 25%;
	}
</style>