<template>
    <div id="app">
        <div class="container">
            <h1 class="bg-light p-4 mb-4 text-center display-4 mt-4">Gerenciamento de Pacientes</h1>
            <button class="btn btn-primary mb-4" @click="abrirModal">Novo Paciente</button>

            <div class="overlay" :class="{ 'show': modalAberto }"></div>

            <div class="modal" :class="{ 'show': modalAberto }" tabindex="-1" role="dialog" style="display: none;" ref="modal">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Novo Paciente</h5>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="nome">Nome:</label>
                                <input type="text" class="form-control" v-model="novoPaciente.name" :class="{ 'is-invalid': submitted && !novoPaciente.name }" required>
                                <span v-if="submitted && !novoPaciente.name" class="invalid-feedback">Nome &eacute; obrigat&oacute;rio</span>
                            </div>
                            <div class="form-group">
                                <label for="idade">Idade:</label>
                                <input type="number" class="form-control" v-model="novoPaciente.age" :class="{ 'is-invalid': submitted && !novoPaciente.age }" required>
                                <span v-if="submitted && !novoPaciente.age" class="invalid-feedback">Idade &eacute; obrigat&oacute;rio</span>
                            </div>
                            <div class="form-group">
                                <label for="sexo">Sexo:</label>
                                <select class="form-control" v-model="novoPaciente.sex" :class="{ 'is-invalid': submitted && !novoPaciente.sex }" required>
                                    <option value="" disabled>SELECIONE</option>
                                    <option value="MASCULINO">MASCULINO</option>
                                    <option value="FEMININO">FEMININO</option>
                                </select>
                                <span v-if="submitted && !novoPaciente.sex" class="invalid-feedback">Sexo &eacute; obrigat&oacute;rio</span>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" @click="adicionarNovoPaciente">Salvar</button>
                            <button type="button" class="btn btn-secondary" @click="fecharModal">Cancelar</button>
                        </div>
                    </div>
                </div>
            </div>


            <div class="overlay" :class="{ 'show': modalEditarAberto  }"></div>

            <div class="modal" :class="{ 'show': modalEditarAberto }" tabindex="-1" role="dialog" style="display: none;" ref="modalEditar">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Editar Paciente</h5>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="nome">Nome:</label>
                                <input type="text" class="form-control" v-model="pacienteEditado.name" :class="{ 'is-invalid': submittedEdit && !pacienteEditado.name }" required>
                                <span v-if="submittedEdit && !pacienteEditado.name" class="invalid-feedback">Nome &eacute; obrigat&oacute;rio</span>
                            </div>
                            <div class="form-group">
                                <label for="idade">Idade:</label>
                                <input type="number" class="form-control" v-model="pacienteEditado.age" :class="{ 'is-invalid': submittedEdit && !pacienteEditado.age }" required>
                                <span v-if="submittedEdit && !pacienteEditado.age" class="invalid-feedback">Idade &eacute; obrigat&oacute;rio</span>
                            </div>
                            <div class="form-group">
                                <label for="sexo">Sexo:</label>
                                <select class="form-control" v-model="pacienteEditado.sex" :class="{ 'is-invalid': submittedEdit && !pacienteEditado.sex }" required>
                                    <option value="" disabled>SELECIONE</option>
                                    <option value="MASCULINO">MASCULINO</option>
                                    <option value="FEMININO">FEMININO</option>
                                </select>
                                <span v-if="submittedEdit && !pacienteEditado.sex" class="invalid-feedback">Sexo &eacute; obrigat&oacute;rio</span>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" @click="salvarEdicaoPaciente">Salvar</button>
                            <button type="button" class="btn btn-secondary" @click="fecharModalEditar">Cancelar</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="table-responsive">
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                            <th>Nome</th>
                            <th>Idade</th>
                            <th>Sexo</th>
                            <th>Exame1</th>
                            <th>Exame2</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="paciente in pacientes" :key="paciente.id">
                            <td>{{ paciente.name }}</td>
                            <td>{{ paciente.age }}</td>
                            <td>{{ paciente.sex }}</td>
                            <td :class="getExame1Class(paciente)">{{ getExame1Text(paciente) }}</td>
                            <td :class="getExame2Class(paciente)">{{ getExame2Text(paciente) }}</td>
                            <td>
                                <button class="btn btn-secondary" @click="editarPaciente(paciente)">Editar</button>
                                &nbsp;
                                <button class="btn btn-danger" @click="excluirPaciente(paciente)">Excluir</button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>

<script>
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
                modalEditarAberto: false,
                submitted: false,
                submittedEdit: false

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
                this.modalEditarAberto = true; 
                document.body.classList.add('modal-open');
                this.$refs.modalEditar.style.display = 'block'; 
            },

            async salvarEdicaoPaciente() {

                this.submittedEdit = true;

                if (!this.pacienteEditado.name || !this.pacienteEditado.age || !this.pacienteEditado.sex) {
                    return; 
                }

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
                this.$refs.modalEditar.style.display = 'none'; 
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
                this.submitted = true; 
               
                if (!this.novoPaciente.name || !this.novoPaciente.age || !this.novoPaciente.sex) {
                    return; 
                }
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
                    return 'Valor acima de 20';
                }
            },
            getExame2Class(paciente) {
                if (paciente.age < 12 || (paciente.age >= 12 && paciente.age <= 18)) {
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
                } else if (paciente.sex === 'MASCULINO' && paciente.age >= 18) {
                    return 'HOMEM ADULTO';
                } else if (paciente.sex === 'FEMININO' && paciente.age >= 18) {
                    return 'MULHER ADULTA';
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
</script>

<style>

    .overlay {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.5); 
        z-index: 999; 
        display: none;
    }

        .overlay.show {
            display: block; 
        }

    .modal.show {
        display: block;
    }

    .table-success {
        background-color: #d4edda;
    }

    .table-danger {
        background-color: #f8d7da;
    }

    
</style>
