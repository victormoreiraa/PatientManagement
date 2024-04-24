export default {
    data() {
        return {
            pacientes: [],
            novoPaciente: {
                name: '',
                age: null,
                sex: ''
            },
            modalAberto: false,
            pacienteEditado: {
                id: null,
                name: '',
                age: null,
                sex: ''
            },
            modalEditarAberto: false

        };
    },
    created() {
        this.carregarPacientes();
    },
    methods: {
        async carregarPacientes() {
            try {
                const response = await fetch('http://localhost:5111/api/patient/');
                if (!response.ok) {
                    throw new Error('Erro ao carregar pacientes');
                }
                const data = await response.json();
                this.pacientes = data;
            } catch (error) {
                console.error('Erro ao carregar pacientes:', error);
            }
        },
        async editarPaciente(paciente) {
            this.pacienteEditado = { ...paciente };
            this.modalEditarAberto = true; // Altera a visibilidade do modal de edição
            document.body.classList.add('modal-open');
            this.$refs.modalEditar.style.display = 'block'; // Exibe o modal de edição
        },

        async salvarEdicaoPaciente() {
            try {
                const response = await fetch(`http://localhost:5111/api/patient/${this.pacienteEditado.id}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(this.pacienteEditado)
                });

                if (!response.ok) {
                    throw new Error('Erro ao salvar edição do paciente');
                }

                console.log("Paciente editado com sucesso!");
                this.fecharModalEditar();
                this.carregarPacientes();
            } catch (error) {
                console.error('Erro ao salvar edição do paciente:', error);
            }
        },

        fecharModalEditar() {
            this.modalEditarAberto = false;
            document.body.classList.remove('modal-open');
            this.$refs.modalEditar.style.display = 'none'; // Esconde o modal de edição
        },
        async excluirPaciente(paciente) {
            try {
                const response = await fetch(`http://localhost:5111/api/patient/${paciente.id}`, {
                    method: 'DELETE'
                });
                if (!response.ok) {
                    throw new Error('Erro ao excluir paciente');
                }
                console.log("Paciente excluído com sucesso!");
                this.carregarPacientes();
            } catch (error) {
                console.error('Erro ao excluir paciente:', error);
            }
        },
        async adicionarNovoPaciente() {
            try {
                if (!this.novoPaciente) {
                    throw new Error('Objeto novoPaciente não foi inicializado corretamente.');
                }

                const response = await fetch('http://localhost:5111/api/patient/', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(this.novoPaciente)
                });

                if (!response.ok) {
                    throw new Error('Erro ao adicionar novo paciente');
                }

                console.log("Novo paciente adicionado com sucesso!");
                this.fecharModal();
                this.carregarPacientes();
            } catch (error) {
                console.error('Erro ao adicionar novo paciente:', error);
            }
        },
        getExame1Class(paciente) {
            if (paciente.age < 10 && paciente.age > 5) {
                return 'table-success';
            } else if (paciente.sex === 'MASCULINO' && paciente.age >= 10 && paciente.age <= 15) {
                return 'table-success';
            } else if (paciente.sex === 'FEMININO' && paciente.age >= 10 && paciente.age <= 20) {
                return 'table-success';
            } else {
                return 'table-danger';
            }
        },
        getExame1Text(paciente) {
            if (paciente.age <= 10 && paciente.age >= 5) {
                return 'Valor entre 5 e 10.';
            } else if (paciente.sex === 'MASCULINO' && paciente.age >= 10 && paciente.age <= 15) {
                return 'Valor entre 10 e 15.';
            } else if (paciente.sex === 'FEMININO' && paciente.age >= 10 && paciente.age <= 20) {
                return 'Valor entre 10 e 20.';
            } else {
                return 'Valor inválido';
            }
        },
        getExame2Class(paciente) {
            if (paciente.age < 12) {
                return 'table-success';
            } else if (paciente.age >= 12 && paciente.age <= 18) {
                return 'table-success';
            } else {
                return 'table-danger';
            }
        },
        getExame2Text(paciente) {
            if (paciente.age < 12) {
                return 'CRIANÇA';
            } else if (paciente.age >= 12 && paciente.age <= 18) {
                return 'ADOLESCENTE';
            } else {
                return 'ADULTO';
            }
        },
        abrirModal() {
            this.modalAberto = true;
            document.body.classList.add('modal-open');
            this.$refs.modal.style.display = 'block';
        },
        fecharModal() {
            this.modalAberto = false;
            document.body.classList.remove('modal-open');
            this.$refs.modal.style.display = 'none';
        }

    }
};