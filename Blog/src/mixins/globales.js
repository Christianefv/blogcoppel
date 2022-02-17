const globales = {
    methods:{
        usuarioGlobal(){
            let lsToken = this.lstokenWebUsuarios();
            let user = JSON.parse(lsToken.user)
            return user
        },
        lstokenWebUsuarios()
        {
            let lsToken = JSON.parse(window.localStorage.tokenWebUsuarios);
            return lsToken;
        }
    }
}

export default globales