import React, { Component } from 'react';
import { cpfMask } from './Mask'

export class CalculoImpostoDeRenda extends Component {
    static displayName = CalculoImpostoDeRenda.name;

  constructor(props) {
      super(props);

      this.state = {

          'insert': { Cpf: '', Nome: '', QuantidadeDependente: '', Renda: '' },
          'items': [],
          'calculos': { SalarioMinimo: '' },
          'valoresIR' : []
      }

 
  }

    componentDidMount() {
        this.ObterListaContribuintes();
    }

    componentDidUpdate() {
        this.ObterListaContribuintes();
    }

    //Recebe a lista de todos os contribuintes adicionados
    ObterListaContribuintes() {
        fetch('/api/Contribuinte')
            .then(results => results.json())
            .then(results => this.setState({ 'items': results }))
    }

    // Handle de alteração dos inputs
    handleChange = event => {
        this.setState({ [event.target.name]: event.target.value })
    }

    handleCPF = event => {
        this.setState({ Cpf: cpfMask(event.target.value) })

    }
    // Chama o Web API para inserção de um contribuinte
    SalvarContribuinte = event => {
        event.preventDefault();
        const url = "/api/Contribuinte";

        if (this.ValidarCampos(this.state.Cpf)) {
            var data = {
                Cpf: this.state.Cpf,
                Nome: this.state.Nome,
                QuantidadeDependente: this.state.QuantidadeDependente,
                Renda: this.state.Renda.replace(/,/g, ".")
            }
         
            fetch(url, {
                method: 'POST',
                body: JSON.stringify(data), 
                headers: {
                    'Accept': 'application/json; charset=utf-8',
                    'Content-Type': 'application/json;charset=UTF-8'
                }
            });

            this.LimparCamposFormulario();
        }
    };

    // Limpa os campos do formulário
    LimparCamposFormulario() {
        document.getElementById("Cpf").value = "";
        document.getElementById("Nome").value = "";
        document.getElementById("QuantidadeDependente").value = "";
        document.getElementById("Renda").value = "";
        this.setState({ Cpf: "" })
    }

    // Chama o Web API que calcula e retorna os valores de IR
    ObterListaDeValoresIR = event => {
        event.preventDefault();
        const url = "/api/Calculo";
        if (this.state.items.length > 0) {
            var data = this.state.SalarioMinimo.replace(/[.]/g, ",");

            fetch(url,
                {
                    method: 'Post',
                    body: data,
                    headers: {
                        'Accept': 'application/json; charset=utf-8',
                        'Content-Type': 'application/json;charset=UTF-8'
                    }
                })
                .then(results => results.json())
                .then(results => this.setState({ 'valoresIR': results }))
        }
        else {
            alert("Adicione pelo menos um contribuinte antes de calcular o imposto de renda");
        }
    }

    // Limpa os campos do formulário
    ValidarCampos(valorCampoCpf) {
        var validado = true;

        if (valorCampoCpf.length < 14) {
            alert("CPF precisa ser preenchido corretamente");
            validado = false;
        }

        return validado;
    }

  render() {
    const { Cpf } = this.state
    return (
      <div>
        <h1>Imposto de Renda</h1>

            <strong>Adicione os contribuintes</strong>
            <br />
            <form onSubmit={this.SalvarContribuinte} id="formularioContribuinte">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="Cpf">CPF</label><br />
                            <input type="text" maxLength="14"  class="form-control" value={Cpf} name="Cpf" onChange={this.handleChange} onKeyDown={this.handleCPF} id="Cpf" required />
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="Nome">Nome</label><br />
                            <input type="text" maxLength="255" name="Nome" class="form-control" onChange={this.handleChange} id="Nome" required />
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="QuantidadeDependente">Quantidade de dependentes</label><br />
                            <input type="number" class="form-control" name="QuantidadeDependente" onChange={this.handleChange} id="QuantidadeDependente" min="0" max="999" required />
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="Renda">Renda bruta mensal (R$)</label><br />
                            <input type="number" class="form-control" name="Renda" step=".01" id="Renda" onChange={this.handleChange} required />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <button className="btn btn-primary form-control" id="submitContribuinte" type="submit">Adicionar contribuinte</button>
                        </div>
                    </div>
                </div>
                
            </form>
           
            <br /><br />
            <strong>Lista de Contribuintes adicionados</strong>
            <br />
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>CPF</th>
                        <th>Nome</th>
                        <th>Qt. Dependentes</th>
                        <th>Renda (R$)</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        this.state.items.map(
                            function (item, index)
                            {
                                return (<tr><td>{item.Cpf}</td><td>{item.Nome}</td><td>{item.QuantidadeDependente}</td><td>{item.Renda.toFixed(2).replace(/[.]/g, ",")}</td></tr>)
                            }
                        )
                    }
                </tbody>
            </table>

            <br /><br />
            <form onSubmit={this.ObterListaDeValoresIR} id="formularioCalculo">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="salarioMinimo">Salario minimo (R$)</label><br />
                            <input class="form-control" type="number" name="SalarioMinimo" step=".01" id="SalarioMinimo" onChange={this.handleChange} required />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <button className="btn btn-primary form-control" id="submitCalculo" type="submit">Calcular IR</button>
                        </div>
                    </div>
                </div>
            </form>
            <br /><br />
            <strong>Calculo de IR</strong>
            <br />
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>CPF</th>
                        <th>Nome</th>
                        <th>Valor IR (R$)</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        this.state.valoresIR.map(
                            function (item, index) {
                                return (<tr><td>{item.Cpf}</td><td>{item.Nome}</td><td>{item.ValorIR.toFixed(2).replace(/[.]/g, ",")}</td></tr>)
                            }
                        )
                    }
                </tbody>
            </table>
            <br />
            <br />
        </div>


    );
  }
}
