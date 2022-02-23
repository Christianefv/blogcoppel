const sidebarmx = {
    methods: {
        togleMenu(opcion) {
            this.$emit("togleMenu", opcion);
       },
    scrollMeTo(refName) {
        let element = this.$refs[refName];
        let top = element.offsetTop;

        window.scrollTo(0, top);
    }
    },  
 }
 
 export default sidebarmx;