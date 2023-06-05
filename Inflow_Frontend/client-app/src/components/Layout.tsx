import React, {RefObject} from 'react';
import './Layout.scss'
import {Route} from "react-router-dom";
import Section from './Section/Section';

export const LAYOUT_REF: RefObject<any> = React.createRef();

function Layout() {

    return (
        <div ref={LAYOUT_REF} className="layout">
        </div>
    );
}

export default Layout;